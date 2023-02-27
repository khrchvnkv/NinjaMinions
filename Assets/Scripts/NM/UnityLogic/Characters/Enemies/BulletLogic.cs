using System;
using NM.Services.Factory;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    public class BulletLogic : MonoBehaviour, IClearable
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
        
        private float _speed;
        private Vector3 _direction;
        private int _damage;
        
        public void Construct(BulletParams bulletParams)
        {
            _speed = bulletParams.Speed;
            _direction = bulletParams.Direction;
            _damage = bulletParams.Damage;
        }
        public void Clear()
        {
            Destroy(gameObject);
        }
        private void FixedUpdate()
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
            gameObject.SetActive(false);
        }
    }
}