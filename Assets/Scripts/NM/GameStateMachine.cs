using System;
using System.Collections.Generic;
using NM.LoadingView;
using NM.Services;
using NM.Services.Factory;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.Services.StaticData;
using NM.States;

namespace NM
{
    public class GameStateMachine
    {
        private readonly LoadingCurtain _loadingCurtain;
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(SceneLoader sceneLoader, LoadingCurtain loadingCurtain, AllServices services)
        {
            _loadingCurtain = loadingCurtain;
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader, loadingCurtain,
                    services.Single<GameFactory>(), services.Single<PersistentProgressService>(),
                    services.Single<StaticDataService>()),
                [typeof(LoadProgressState)] = new LoadProgressState(this, 
                    services.Single<PersistentProgressService>(),
                    services.Single<SaveLoadService>()),
                [typeof(GameLoopState)] = new GameLoopState(this, services.Single<InputService>()),
            };
        }
        public void Enter<TState>() where TState : class, IState
        {
            var state = ChangeState<TState>();
            state.Enter();
        }
        public void Enter<TState>(string payload) where TState : class, IPayloadedState
        {
            var state = ChangeState<TState>();
            state.Enter(payload);
        }
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}