namespace NM.CoreLogic.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
    public interface IExitableState
    {
        void Exit();
    }
    public interface IPayloadedState : IExitableState
    {
        void Enter(string payload);
    }
}