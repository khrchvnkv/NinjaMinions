using System.Collections.Generic;
using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies.Behaviour.Stalker;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Sniper
{
    public class SniperEnemy : EnemyLogic<EmptyBehaviour, SniperStaticData>
    {
        [SerializeField] private Transform _bulletSpawnPoint;
        private SniperBehaviour _sniperBehaviour;

        public override void Construct(GameFactory gameFactory, string id, EnemyStaticData enemyData, List<Vector3> patrolPoints)
        {
            base.Construct(gameFactory, id, enemyData, patrolPoints);
            IdleBehaviour = new EmptyBehaviour();
            EnterBehaviour(IdleBehaviour);
            var spawnPoint = transform;
            _sniperBehaviour = new SniperBehaviour(gameFactory, StaticData, _bulletSpawnPoint, _bulletSpawnPoint);
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
            _sniperBehaviour.SetTarget(minion.transform);
            EnterBehaviour(_sniperBehaviour);
        }
        private void StopSniperBehaviour(MinionContainer minion)
        {
            EnterBehaviour(IdleBehaviour);
        }
    }
}