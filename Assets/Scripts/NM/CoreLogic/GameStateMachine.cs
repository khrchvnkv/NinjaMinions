using System;
using System.Collections.Generic;
using NM.CoreLogic.LoadingView;
using NM.CoreLogic.Services;
using NM.CoreLogic.Services.AssetManagement;
using NM.CoreLogic.Services.Factory;
using NM.CoreLogic.States;

namespace NM.CoreLogic
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
                    services.Single<GameFactory>()),
                [typeof(GameLoopState)] = new GameLoopState(this),
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