using NM.Services.Factory;
using NM.Services.Pool;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    public class BulletLogic : MonoBehaviour, IFixedUpdater, IClearable, IPoolObject
    {
        public struct BulletParams
        {
            public readonly float Speed;
            public readonly Vector3 Direction;
            public readonly int Damage;

            public BulletParams(float speed, Vector3 direction, int damage)
            {
                Speed = speed;
                Direction = direction;
                Damage = damage;
            }
        }

        [SerializeField] private Transform _movingTransform;

        private IUpdateRunner _updateRunner;
        private GameFactory _gameFactory;
        private float _speed;
        private Vector3 _direction;
        private int _damage;
        
        public void Construct(IUpdateRunner updateRunner, GameFactory gameFactory, BulletParams bulletParams)
        {
            _updateRunner = updateRunner;
            _gameFactory = gameFactory;
            _speed = bulletParams.Speed;
            _direction = bulletParams.Direction;
            _damage = bulletParams.Damage;
            _updateRunner.AddFixedUpdate(this);
        }
        public void Clear() => RemoveToPool();
        public void FixedUpdateLogic()
        {
            var offset = _direction * _speed * Time.deltaTime;
            _movingTransform.position += offset;
        }
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent(out MinionContainer minion))
            {
                minion.MinionHp.TakeDamage(_damage);
            }
            RemoveToPool();
        }
        private void RemoveToPool()
        {
            _updateRunner.RemoveFixedUpdate(this);
            _gameFactory.AddToPool<BulletLogic>(gameObject);
        }
    }
}