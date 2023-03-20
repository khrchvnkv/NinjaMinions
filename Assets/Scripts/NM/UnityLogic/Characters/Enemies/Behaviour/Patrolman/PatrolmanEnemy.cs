using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies.StateRecovering;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman
{
    public class PatrolmanEnemy : EnemyLogic<PatrolBehaviour, PatrolmanStaticData>
    {
        private PatrolmanStateRecovery _stateRecovery;
        
        public override void Construct(IUpdateRunner updateRunner, GameFactory gameFactory, string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(updateRunner, gameFactory, id, enemyData, patrolPoints);
            IdleBehaviour = new PatrolBehaviour(Agent, transform, patrolPoints);
            if (_stateRecovery == null) _stateRecovery = new PatrolmanStateRecovery(EnterBehaviour);
        }
        public override void Clear()
        {
            base.Clear();
            GameFactory.AddToPool<PatrolmanEnemy>(gameObject);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            AttackZone.OnAggroZoneEntered += AttackAction;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            AttackZone.OnAggroZoneEntered -= AttackAction;
        }
        protected override string GenerateStateMeta() => 
            _stateRecovery.GenerateStateMeta(IdleBehaviour);
        protected override void RestoreBehaviourState(string stateMeta) => 
            _stateRecovery.RestoreBehaviourState(stateMeta, IdleBehaviour);
    }
}