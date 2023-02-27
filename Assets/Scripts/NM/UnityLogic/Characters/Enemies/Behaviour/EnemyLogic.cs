using System.Collections.Generic;
using NM.Data;
using NM.Services.Factory;
using NM.Services.PersistentProgress;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.AI;

namespace NM.UnityLogic.Characters.Enemies.Behaviour
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyLogic<TBehaviour, TData> : MonoBehaviour, IEnemy, IClearable, ISavedProgressWriter 
        where TBehaviour : IEnemyBehaviour
        where TData : EnemyStaticData
    {
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] protected AggroZone AttackZone;

        private string _id;
        private IEnemyBehaviour _currentBehaviour;
        private bool _isDied;

        protected TBehaviour IdleBehaviour;
        protected TData StaticData;

        public virtual void Construct(GameFactory gameFactory, string id, 
            EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            _id = id;
            StaticData = (TData)enemyData;
            Agent.speed = StaticData.Speed;
            AttackZone.SetZoneScale(enemyData.AttackDistance);
        }
        public void LoadProgress(SaveSlotData slotData)
        {
            foreach (var enemyData in slotData.EnemiesData)
            {
                if (enemyData.Id == _id)
                {
                    Agent.Warp(enemyData.Position.AsUnityVector());
                    var isAlive = !enemyData.IsDied;
                    Agent.gameObject.SetActive(isAlive);
                    _isDied = !isAlive;
                    return;
                }
            }
        }
        public void SaveProgress(SaveSlotData slotData)
        {
            var position = Agent.transform.position.AsVector3Data();
            foreach (var enemyData in slotData.EnemiesData)
            {
                if (enemyData.Id == _id)
                {
                    enemyData.Position = position;
                    enemyData.IsDied = _isDied;
                    return;
                }
            }
            slotData.EnemiesData.Add(new EnemyData(_id, position, _isDied));
        }
        public void Clear() => Destroy(gameObject);
        protected void EnterBehaviour(IEnemyBehaviour behaviour)
        {
            _currentBehaviour?.Exit();
            _currentBehaviour = behaviour;
            _currentBehaviour.Enter();
        }
        protected void AttackAction(MinionContainer minion)
        {
            minion.MinionHp.TakeDamage(StaticData.Damage);
            gameObject.SetActive(false);
            _isDied = true;
        }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable()
        {
            _currentBehaviour?.Exit();
        }
        private void Update()
        {
            _currentBehaviour?.UpdateBehaviour();
        }
    }
}