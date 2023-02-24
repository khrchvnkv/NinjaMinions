using System;
using UnityEngine;

namespace NM.StaticData
{
    [Serializable]
    public class MinionSpawnerData
    {
        public string Id;
        public Vector3 SpawnPosition;
        
        public MinionSpawnerData(string id, Vector3 spawnPosition)
        {
            Id = id;
            SpawnPosition = spawnPosition;
        }
    }
}