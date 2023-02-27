using System;
using System.Collections.Generic;
using NM.Data;
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
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _activeState;

        public GameStateMachine(ICoroutineRunner coroutineRunner, SceneLoader sceneLoader, 
            LoadingCurtain loadingCurtain, AllServices services)
        {
            _states = new Dictionary<Type, IExitableState>
            {
                [typeof(BootstrapState)] = new BootstrapState(coroutineRunner, this, sceneLoader, services),
                [typeof(LoadLevelState)] = new LoadLevelState(coroutineRunner, this, sceneLoader, loadingCurtain,
                    services.Single<GameFactory>(), services.Single<StaticDataService>()),
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
        public void Enter<TState>(SaveSlotData slot) where TState : class, IPayloadedState
        {
            var state = ChangeState<TState>();
            state.Enter(slot);
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