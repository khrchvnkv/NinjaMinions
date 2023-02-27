using NM.Data;
using NM.LoadingView;
using NM.Services.Factory;
using NM.Services.StaticData;
using UnityEngine.SceneManagement;

namespace NM.States
{
    public class LoadLevelState : IPayloadedState
    {
        private readonly ICoroutineRunner _coroutineRunner;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameFactory _gameFactory;
        private readonly StaticDataService _staticDataService;

        public LoadLevelState(ICoroutineRunner coroutineRunner, GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, GameFactory gameFactory, StaticDataService staticDataService)
        {
            _coroutineRunner = coroutineRunner;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _staticDataService = staticDataService;
        }
        public void Enter(SaveSlotData slot)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(slot.Level, () => OnLoaded(slot));
        }
        private void OnLoaded(SaveSlotData slot)
        {
            InitPool();
            InitHud();
            InitSpawners();
            InformProgressReaders(slot);
            _gameStateMachine.Enter<GameLoopState>();
        }
        private void InitPool()
        {
            _gameFactory.CreatePool(_coroutineRunner);
        }
        private void InitHud()
        {
            _gameFactory.CreateHud();
        }
        private void InitSpawners()
        {
            _gameFactory.CreateMinionsMover();
            string sceneKey = SceneManager.GetActiveScene().name;
            var levelStaticData = _staticDataService.GetLevelData(sceneKey);
            foreach (var minionSpawner in levelStaticData.MinionSpawners)
            {
                _gameFactory.CreateMinionSpawner(minionSpawner);
            }
            foreach (var enemySpawner in levelStaticData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(enemySpawner);
            }
        }
        private void InformProgressReaders(SaveSlotData slot)
        {
            foreach (var reader in _gameFactory.ProgressReaders)
            {
                reader.LoadProgress(slot);
            }
        }
        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}