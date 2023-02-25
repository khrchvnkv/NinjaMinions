using NM.Services.Factory;
using UnityEngine;

namespace NM.UnityLogic.Characters.Minion
{
    public class MinionContainer : MonoBehaviour, IClearable
    {
        [SerializeField] private MinionHp _minionHp;
        [SerializeField] private MinionMove _minionMove;

        public MinionHp MinionHp => _minionHp;
        public MinionMove MinionMove => _minionMove;
        
        public void Construct(string id, int maxHp, float movementSpeed)
        {
            _minionHp.Construct(id, maxHp);
            _minionMove.Construct(id, movementSpeed);
        }
        public void Clear() => Destroy(gameObject);
    }
}