using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "Sniper", menuName = "StaticData/Enemy/Sniper")]
    public class SniperStaticData : EnemyStaticData
    {
        public override EnemyTypeId EnemyType => EnemyTypeId.Sniper;

        public float ShootCooldown;
        public float BulletSpeed;
        public GameObject Bullet;
    }
}