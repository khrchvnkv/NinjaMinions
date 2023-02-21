using NM.CoreLogic.LoadingView;
using NM.CoreLogic.Services.Factory;

namespace NM.CoreLogic.States
{
    public class LoadLevelState : IPayloadedState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingCurtain _loadingCurtain;
        private readonly GameFactory _gameFactory;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, GameFactory gameFactory)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }
        public void Enter(string sceneName)
        {
            _loadingCurtain.Show();
            _sceneLoader.Load(sceneName, OnLoaded);
        }
        private void OnLoaded()
        {
            var hero = _gameFactory.CreateHero();
            _gameFactory.CreateHUD();
            _gameStateMachine.Enter<GameLoopState>();
        }
        public void Exit()
        {
            _loadingCurtain.Hide();
        }
    }
}