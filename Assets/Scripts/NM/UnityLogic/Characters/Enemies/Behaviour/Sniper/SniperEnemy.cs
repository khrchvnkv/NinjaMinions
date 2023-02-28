using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies.Behaviour.Stalker;
using NM.UnityLogic.Characters.Enemies.StateRecovering;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Sniper
{
    public class SniperEnemy : EnemyLogic<EmptyBehaviour, SniperStaticData>
    {
        [SerializeField] private Transform _bulletSpawnPoint;

        private AggroStateRecovery _stateRecovery;
        private SniperBehaviour _sniperBehaviour;
        private bool _isAggro;

        public override void Construct(GameFactory gameFactory, string id, EnemyStaticData enemyData, 
            List<Vector3> patrolPoints)
        {
            base.Construct(gameFactory, id, enemyData, patrolPoints);
            IdleBehaviour = new EmptyBehaviour();
            _isAggro = false;
            if (_stateRecovery == null) _stateRecovery = new AggroStateRecovery(EnterBehaviour);
        }
        public override void Clear()
        {
            base.Clear();
            GameFactory.AddToPool<SniperEnemy>(gameObject);
        }
        protected override void OnEnable()
        {
            base.OnEnable();
            AttackZone.OnAggroZoneEntered += StartSniperBehaviour;
            AttackZone.OnAggroZoneExited += StopSniperBehaviour;
        }
        protected override void OnDisable()
        {
            base.OnDisable();
            AttackZone.OnAggroZoneEntered -= StartSniperBehaviour;
            AttackZone.OnAggroZoneExited -= StopSniperBehaviour;
        }
        private void StartSniperBehaviour(MinionContainer minion)
        {
            _isAggro = true;
            _sniperBehaviour = new SniperBehaviour(GameFactory, StaticData, _bulletSpawnPoint, minion);
            EnterBehaviour(_sniperBehaviour);
        }
        private void StopSniperBehaviour(MinionContainer minion)
        {
            _isAggro = false;
            EnterBehaviour(IdleBehaviour);
        }
        protected override string GenerateStateMeta() => _stateRecovery.GenerateStateMeta(_isAggro, _sniperBehaviour);
        protected override void RestoreBehaviourState(string stateMeta) => 
            _stateRecovery.RestoreBehaviourState(stateMeta, IdleBehaviour, StartSniperBehaviour, GameFactory);
    }
}