using System;
using UnityEngine;

namespace NM.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyStaticData.EnemyTypeId EnemyTypeId;
        public Vector3 SpawnPosition;

        public EnemySpawnerData(string id, EnemyStaticData.EnemyTypeId enemyTypeId, Vector3 spawnPosition)
        {
            Id = id;
            EnemyTypeId = enemyTypeId;
            SpawnPosition = spawnPosition;
        }
    }
}