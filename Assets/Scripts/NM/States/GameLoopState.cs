using NM.Services.Input;

namespace NM.States
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly InputService _inputService;

        public GameLoopState(GameStateMachine gameStateMachine, InputService inputService)
        {
            _gameStateMachine = gameStateMachine;
            _inputService = inputService;
        }
        public void Enter()
        {
            _inputService.Activate();
        }
        public void Exit()
        {
            
        }
    }
}