using NM.Services;
using NM.Services.AssetManagement;
using NM.Services.Factory;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.Services.StaticData;

namespace NM.States
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
        private void EnterLoadLevelState() => _gameStateMachine.Enter<LoadProgressState>();
        private void RegisterServices()
        {
            _services.RegisterSingle<InputService>(new StandaloneInputService());
            _services.RegisterSingle<AssetProvider>(new AssetProvider());
            _services.RegisterSingle<PersistentProgressService>(new PersistentProgressService());
            RegisterStaticData();
            _services.RegisterSingle<GameFactory>(
                new GameFactory(AllServices.Container.Single<AssetProvider>(),
                                            AllServices.Container.Single<StaticDataService>(),
                                            AllServices.Container.Single<InputService>()));
            _services.RegisterSingle<SaveLoadService>(new SaveLoadService(_services.Single<GameFactory>(),
                _services.Single<PersistentProgressService>()));
        }
        private void RegisterStaticData()
        {
            var staticDataService = new StaticDataService();
            staticDataService.LoadData();
            _services.RegisterSingle<StaticDataService>(staticDataService);
        }
        public void Exit()
        {
        
        }
    }
}