namespace NM
{
    public interface IUpdater
    {
        void UpdateLogic();
    }
    public interface IFixedUpdater
    {
        void FixedUpdateLogic();
    }
    public interface IUpdateRunner
    {
        void AddUpdate(IUpdater updater);
        void RemoveUpdate(IUpdater updater);
        void AddFixedUpdate(IFixedUpdater fixedUpdater);
        void RemoveFixedUpdate(IFixedUpdater fixedUpdater);
    }
}