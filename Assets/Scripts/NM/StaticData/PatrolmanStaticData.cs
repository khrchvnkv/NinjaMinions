using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "Patrolman", menuName = "StaticData/Enemy/Patrolman")]
    public class PatrolmanStaticData : EnemyStaticData
    {
        public override EnemyTypeId EnemyType => EnemyTypeId.Patrolman;
    }
}