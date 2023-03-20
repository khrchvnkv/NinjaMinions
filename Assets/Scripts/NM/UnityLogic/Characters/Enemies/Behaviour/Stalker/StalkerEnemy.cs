using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies.StateRecovering;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Stalker
{
    public class StalkerEnemy : EnemyLogic<EmptyBehaviour, StalkerStaticData>
    {
        [SerializeField] private AggroZone _aggroZone;

        private AggroStateRecovery _stateRecovery;
        private StalkerBehaviour _stalkerBehaviour;
        private bool _isAggro;
        
        public override void Construct(IUpdateRunner updateRunner, GameFactory gameFactory, string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(updateRunner, gameFactory, id, enemyData, patrolPoints);
            _aggroZone.SetZoneScale(StaticData.AggroDistance);
            IdleBehaviour = new EmptyBehaviour();
            _isAggro = false;
            if (_stateRecovery == null) _stateRecovery = new AggroStateRecovery(EnterBehaviour);
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
            _isAggro = true;
            _stalkerBehaviour = new StalkerBehaviour(Agent, minion);
            EnterBehaviour(_stalkerBehaviour);
        }
        protected override string GenerateStateMeta() => _stateRecovery.GenerateStateMeta(_isAggro, _stalkerBehaviour);
        protected override void RestoreBehaviourState(string stateMeta) => 
            _stateRecovery.RestoreBehaviourState(stateMeta, IdleBehaviour, StartStalkerBehaviour, GameFactory);
    }
}