using System.Collections.Generic;
using NM.Data;
using NM.Services.Factory;
using NM.Services.PersistentProgress;
using NM.Services.Pool;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.AI;

namespace NM.UnityLogic.Characters.Enemies.Behaviour
{
    [RequireComponent(typeof(NavMeshAgent))]
    public abstract class EnemyLogic<TBehaviour, TData> : MonoBehaviour, IEnemy, IClearable, ISavedProgressWriter, IPoolObject 
        where TBehaviour : IEnemyBehaviour
        where TData : EnemyStaticData
    {
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] protected AggroZone AttackZone;

        private string _id;
        private IEnemyBehaviour _currentBehaviour;
        private bool _isDied;

        protected GameFactory GameFactory;
        protected TBehaviour IdleBehaviour;
        protected TData StaticData;

        public virtual void Construct(GameFactory gameFactory, string id, 
            EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            GameFactory = gameFactory;
            _id = id;
            StaticData = (TData)enemyData;
            Agent.speed = StaticData.Speed;
            AttackZone.SetZoneScale(enemyData.AttackDistance);
            _isDied = false;
        }
        public void LoadProgress(SaveSlotData slotData)
        {
            foreach (var enemyData in slotData.EnemiesData)
            {
                if (enemyData.Id == _id)
                {
                    WarpTo(enemyData.Position.AsUnityVector(), enemyData.Rotation.AsUnityVector());
                    var isAlive = !enemyData.IsDied;
                    Agent.gameObject.SetActive(isAlive);
                    _isDied = !isAlive;
                    ActivateTriggers();
                    return;
                }
            }
            ActivateTriggers();
        }
        public void SaveProgress(SaveSlotData slotData)
        {
            var agentTransform = Agent.transform;
            var position = agentTransform.position.AsVector3Data();
            var rotation = agentTransform.rotation.eulerAngles.AsVector3Data();
            foreach (var enemyData in slotData.EnemiesData)
            {
                if (enemyData.Id == _id)
                {
                    enemyData.Position = position;
                    enemyData.Rotation = rotation;
                    enemyData.IsDied = _isDied;
                    return;
                }
            }
            slotData.EnemiesData.Add(new EnemyData(_id, position, rotation, _isDied));
        }
        public virtual void Clear()
        {
            DeactivateTriggers();
        }
        protected virtual void ActivateTriggers()
        {
            AttackZone.SetZoneActivity(true);
        }
        protected virtual void DeactivateTriggers()
        {
            AttackZone.SetZoneActivity(false);
        }
        protected void EnterBehaviour(IEnemyBehaviour behaviour)
        {
            _currentBehaviour?.Exit();
            _currentBehaviour = behaviour;
            _currentBehaviour.Enter();
        }
        protected void AttackAction(MinionContainer minion)
        {
            minion.MinionHp.TakeDamage(StaticData.Damage);
            _isDied = true;
            Clear();
        }
        protected virtual void OnEnable() { }
        protected virtual void OnDisable()
        {
            _currentBehaviour?.Exit();
        }
        private void WarpTo(Vector3 position, Vector3 rotation)
        {
            var agentTransform = Agent.transform;
            var quaternion = Quaternion.Euler(rotation);
            agentTransform.position = position;
            agentTransform.rotation = quaternion;
        }
        private void Update()
        {
            _currentBehaviour?.UpdateBehaviour();
        }
    }
}