using NM.LoadingView;
using NM.Services.Factory;
using NM.Services.PersistentProgress;
using NM.Services.StaticData;
using UnityEngine.SceneManagement;

namespace NM.States
{
    public class LoadLevelState : IPayloadedState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameFactory _gameFactory;
        private readonly PersistentProgressService _progressService;
        private readonly StaticDataService _staticDataService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, GameFactory gameFactory, 
            PersistentProgressService progressService, StaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
            _staticDataService = staticDataService;
        }
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        private void OnLoaded()
        {
            InitSpawners();
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }
        private void InitSpawners()
        {
            // Create Minion Mover
            _gameFactory.CreateMinionsMover();
            string sceneKey = SceneManager.GetActiveScene().name;
            var levelStaticData = _staticDataService.GetLevelData(sceneKey);
            foreach (var minionSpawner in levelStaticData.MinionSpawners)
            {
                _gameFactory.CreateMinionSpawner(minionSpawner.SpawnPosition, minionSpawner.Id);
            }
            foreach (var enemySpawner in levelStaticData.EnemySpawners)
            {
                _gameFactory.CreateEnemySpawner(enemySpawner.SpawnPosition, enemySpawner.Id, enemySpawner.EnemyTypeId);
            }
        }
        private void InitGameWorld()
        {
            _gameFactory.CreateHUD();
        }
        private void InformProgressReaders()
        {
            foreach (var reader in _gameFactory.ProgressReaders)
            {
                reader.LoadProgress(_progressService.Progress);
            }
        }
        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}