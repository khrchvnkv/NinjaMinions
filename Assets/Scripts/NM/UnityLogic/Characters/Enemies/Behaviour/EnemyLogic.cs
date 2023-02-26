using System;
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
    public abstract class EnemyLogic<T> : MonoBehaviour, IEnemy, IClearable, ISavedProgressWriter where T : IEnemyBehaviour
    {
        [SerializeField] protected NavMeshAgent Agent;
        [SerializeField] protected AggroZone AggroZone;
        
        private string _id;
        private EnemyStaticData _enemyData;

        protected IEnemyBehaviour CurrentBehaviour;
        protected T IdleBehaviour;
        protected List<Vector3> PatrolPoints;
        protected bool IsDied;

        protected int Damage => _enemyData.Damage;

        public virtual void Construct(string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            _id = id;
            _enemyData = enemyData;
            PatrolPoints = new List<Vector3>(patrolPoints);
            Agent.speed = _enemyData.Speed;
            AggroZone.SetZoneRadius(enemyData.AttackDistance);
        }
        public void LoadProgress(SaveSlotData slotData)
        {
            foreach (var enemyData in slotData.EnemiesData)
            {
                if (enemyData.Id == _id)
                {
                    Agent.transform.position = enemyData.Position.AsUnityVector();
                    var isAlive = !enemyData.IsDied;
                    Agent.gameObject.SetActive(isAlive);
                    IsDied = !isAlive;
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
                    enemyData.IsDied = IsDied;
                    return;
                }
            }
            slotData.EnemiesData.Add(new EnemyData(_id, position, IsDied));
        }
        public void Clear() => Destroy(gameObject);
        protected void EnterBehaviour(IEnemyBehaviour behaviour)
        {
            CurrentBehaviour?.Exit();
            CurrentBehaviour = behaviour;
            CurrentBehaviour.Enter();
        }
        protected abstract void OnAggroZoneEntered(MinionContainer minion);
        private void OnEnable()
        {
            AggroZone.OnAggroZoneEntered += OnAggroZoneEntered;
        }
        private void OnDisable()
        {
            AggroZone.OnAggroZoneEntered -= OnAggroZoneEntered;
        }
        private void Update()
        {
            CurrentBehaviour?.UpdateBehaviour();
        }
    }
}