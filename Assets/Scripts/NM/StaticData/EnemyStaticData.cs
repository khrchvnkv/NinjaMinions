using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "StaticData/EnemyStaticData", order = 0)]
    public class EnemyStaticData : ScriptableObject
    {
        public enum EnemyTypeId
        {
            Patrolman
        }

        public EnemyTypeId EnemyType;
        [Range(0, 10)] public int Damage;
        [Range(0.0f, 10.0f)] public float Speed;
        [Range(0.0f, 5.0f)] public float AttackDistance;
        public GameObject Prefab;
    }
}