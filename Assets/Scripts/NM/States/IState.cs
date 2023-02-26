using NM.Data;

namespace NM.States
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
        void Enter(SaveSlotData slot);
    }
}