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
        protected override void OnEnable()
        {
            base.OnEnable();
            _aggroZone.OnAggroZoneEntered += StartStalkerBehaviour;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            _aggroZone.OnAggroZoneEntered -= StartStalkerBehaviour;
        }
        private void StartStalkerBehaviour(MinionContainer minion)
        {
            _stalkerBehaviour = new StalkerBehaviour(Agent, minion.transform, AttackZone, AttackAction);
            EnterBehaviour(_stalkerBehaviour);
        }
    }
}