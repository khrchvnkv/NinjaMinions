using NM.Services.Factory;
using NM.StaticData;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.Behaviour.Sniper
{
    internal class SniperBehaviour : IAggroBehaviour
    {
        private readonly GameFactory _gameFactory;
        private readonly SniperStaticData _staticData;
        private readonly MinionContainer _minion;
        private readonly Transform _spawnPoint;
        private readonly Transform _target;

        private float _cooldown;

        public string MinionId => _minion.Id;
        
        public SniperBehaviour(GameFactory gameFactory, SniperStaticData staticData, 
            Transform spawnPoint, MinionContainer minion)
        {
            _gameFactory = gameFactory;
            _staticData = staticData;
            _spawnPoint = spawnPoint;
            _minion = minion;
            _target = minion.transform;
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
            _gameFactory.CreateBullet(_staticData.Bullet, _spawnPoint, bulletParams);
            _cooldown = 0.0f;
        }
    }
}