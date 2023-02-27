using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman
{
    public class PatrolmanEnemy : EnemyLogic<PatrolBehaviour, PatrolmanStaticData>
    {
        public override void Construct(GameFactory gameFactory, string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(gameFactory, id, enemyData, patrolPoints);
            IdleBehaviour = new PatrolBehaviour(Agent, transform, patrolPoints);
            EnterBehaviour(IdleBehaviour);
        }
        protected override void OnEnable()
        {
            AttackZone.OnAggroZoneEntered += AttackAction;
        }
        protected override void OnDisable()
        {
            AttackZone.OnAggroZoneEntered -= AttackAction;
        }
    }
}