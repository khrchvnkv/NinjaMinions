using NM.Services.Factory;
using NM.Services.Pool;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionContainer : MonoBehaviour, IClearable, IPoolObject
    {
        [SerializeField] private MinionHp _minionHp;
        [SerializeField] private MinionMove _minionMove;

        public string Id { get; private set; }
        public MinionHp MinionHp => _minionHp;
        public MinionMove MinionMove => _minionMove;

        private GameFactory _gameFactory;
        
        public void Construct(GameFactory gameFactory, string id, int maxHp, float movementSpeed)
        {
            Id = id;
            _gameFactory = gameFactory;
            _minionHp.Construct(id, maxHp);
            _minionMove.Construct(id, movementSpeed);
        }
        public void Clear() => _gameFactory.AddToPool<MinionContainer>(gameObject);
    }
}