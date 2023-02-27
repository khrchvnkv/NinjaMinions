using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Stalker
{
    public class StalkerEnemy : EnemyLogic<EmptyBehaviour, StalkerStaticData>
    {
        [SerializeField] private AggroZone _aggroZone;

        private StalkerBehaviour _stalkerBehaviour;
        
        public override void Construct(GameFactory gameFactory, string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(gameFactory, id, enemyData, patrolPoints);
            _aggroZone.SetZoneScale(StaticData.AggroDistance);
            IdleBehaviour = new EmptyBehaviour();
            EnterBehaviour(IdleBehaviour);
        }
        public override void Clear()
        {
            base.Clear();
            GameFactory.AddToPool<StalkerEnemy>(gameObject);
        }
        protected override void ActivateTriggers()
        {
            base.ActivateTriggers();
            _aggroZone.SetZoneActivity(true);
        }
        protected override void DeactivateTriggers()
        {
            base.DeactivateTriggers();
            _aggroZone.SetZoneActivity(false);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            AttackZone.OnAggroZoneEntered += AttackAction;
            _aggroZone.OnAggroZoneEntered += StartStalkerBehaviour;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            AttackZone.OnAggroZoneEntered -= AttackAction;
            _aggroZone.OnAggroZoneEntered -= StartStalkerBehaviour;
        }
        private void StartStalkerBehaviour(MinionContainer minion)
        {
            _stalkerBehaviour = new StalkerBehaviour(Agent, minion.transform);
            EnterBehaviour(_stalkerBehaviour);
        }
    }
}