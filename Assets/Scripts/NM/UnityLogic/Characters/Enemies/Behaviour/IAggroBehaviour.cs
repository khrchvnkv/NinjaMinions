namespace NM.UnityLogic.Characters.Enemies.Behaviour
{
    public interface IAggroBehaviour : IEnemyBehaviour
    {
        string MinionId { get; }
    }
}