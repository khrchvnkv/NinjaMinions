using NM.Services.Factory;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Sniper
{
    internal class SniperBehaviour : IEnemyBehaviour
    {
        private readonly GameFactory _gameFactory;
        private readonly SniperStaticData _staticData;

        private Transform _parent;
        private Transform _spawnPoint;
        private Transform _target;
        private float _cooldown;

        public SniperBehaviour(GameFactory gameFactory, SniperStaticData staticData,
            Transform parent, Transform spawnPoint)
        {
            _gameFactory = gameFactory;
            _staticData = staticData;
            _parent = parent;
            _spawnPoint = spawnPoint;
        }
        public void SetTarget(Transform target)
        {
            _target = target;
        }
        public void Enter()
        {
        }
        public void UpdateBehaviour()
        {
            _cooldown += Time.deltaTime;
            if (_cooldown >= _staticData.ShootCooldown)
            {
                Shoot();
            }
        }
        public void Exit()
        {
        }
        private void Shoot()
        {
            var position = _spawnPoint.position;
            var direction = _target.position - position;
            direction.y = 0.0f;
            direction.Normalize();
            var bulletParams =
                new BulletLogic.BulletParams(_staticData.BulletSpeed, direction, _staticData.Damage);
            _gameFactory.CreateBullet(_staticData.Bullet, _parent, position, _spawnPoint.rotation,
                bulletParams);
            _cooldown = 0.0f;
        }
    }
}