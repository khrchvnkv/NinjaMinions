using NM.Data;
using NM.LoadingView;
using NM.Services.Factory;
using NM.Services.StaticData;
using UnityEngine.SceneManagement;

namespace NM.States
{
    public class LoadLevelState : IPayloadedState
    {
        private readonly IDontDestroyMarker _dontDestroyMarker;
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameFactory _gameFactory;
        private readonly StaticDataService _staticDataService;

        private bool _isInitialLaunch = true;

        public LoadLevelState(IDontDestroyMarker dontDestroyMarker, GameStateMachine gameStateMachine, 
            SceneLoader sceneLoader, LoadingCurtain loadingCurtain, GameFactory gameFactory, 
            StaticDataService staticDataService)
        {
            _dontDestroyMarker = dontDestroyMarker;
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
            if (_isInitialLaunch) _gameFactory.CreatePool(_dontDestroyMarker);
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
            var readers = _gameFactory.GetProgressReaders();
            foreach (var reader in readers)
            {
                reader.LoadProgress(slot);
            }
        }
        public void Exit()
        {
            if (_isInitialLaunch) _isInitialLaunch = false;
            _loadingCurtain.Hide();
        }
    }
}