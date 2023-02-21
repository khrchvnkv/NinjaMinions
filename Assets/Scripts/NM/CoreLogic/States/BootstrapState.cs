using NM.CoreLogic.Services;
using NM.CoreLogic.Services.AssetManagement;
using NM.CoreLogic.Services.Factory;
using NM.CoreLogic.Services.Input;
using NM.CoreLogic.Services.PersistentProgress;

namespace NM.CoreLogic.States
{
    public class BootstrapState : IState
    {
        private const string Initial = "Init";
        private const string Main = "Main";
        
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices services)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = services;
            
            RegisterServices();
        }
        public void Enter()
        {
            _sceneLoader.Load(Initial, onLoaded: EnterLoadLevelState);
        }
        private void EnterLoadLevelState() => _gameStateMachine.Enter<LoadLevelState>(Main);
        private void RegisterServices()
        {
            _services.RegisterSingle<InputService>(new StandaloneInputService());
            _services.RegisterSingle<AssetProvider>(new AssetProvider());
            _services.RegisterSingle<PersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<GameFactory>(
                new GameFactory(AllServices.Container.Single<AssetProvider>()));
        }
        public void Exit()
        {
        
        }
    }
}