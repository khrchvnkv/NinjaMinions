using NM.CoreLogic.LoadingView;
using NM.CoreLogic.Services.Factory;
using NM.CoreLogic.Services.PersistentProgress;

namespace NM.CoreLogic.States
{
    public class LoadLevelState : IPayloadedState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameFactory _gameFactory;
        private readonly PersistentProgressService _progressService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, GameFactory gameFactory, PersistentProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
            _progressService = progressService;
        }
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _gameFactory.Cleanup();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        private void OnLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            _gameStateMachine.Enter<GameLoopState>();
        }
        private void InitGameWorld()
        {
            //var hero = _gameFactory.CreateHero();
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