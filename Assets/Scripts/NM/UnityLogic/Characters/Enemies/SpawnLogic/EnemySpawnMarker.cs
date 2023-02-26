using System.Collections.Generic;
using NM.StaticData;
using UnityEngine;

namespace NM.UnityLogic.Characters.Enemies.SpawnLogic
{
    public class EnemySpawnMarker : MonoBehaviour
    {
        public EnemyStaticData.EnemyTypeId EnemyTypeId;
        public List<Transform> Points;
    }
}