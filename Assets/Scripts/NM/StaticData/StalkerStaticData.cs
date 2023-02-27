using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "Stalker", menuName = "StaticData/Enemy/Stalker")]
    public class StalkerStaticData : EnemyStaticData
    {
        public override EnemyTypeId EnemyType => EnemyTypeId.Stalker;

        public float AggroDistance;
    }
}