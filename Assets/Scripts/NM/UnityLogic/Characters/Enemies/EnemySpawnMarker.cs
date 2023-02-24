using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        [SerializeField] private EnemyStaticData.EnemyTypeId _enemyType;

        public EnemyStaticData.EnemyTypeId EnemyTypeId => _enemyType;
    }
}