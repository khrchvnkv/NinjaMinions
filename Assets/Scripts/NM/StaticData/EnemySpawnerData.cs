﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace NM.StaticData
{
    [Serializable]
    public class EnemySpawnerData
    {
        public string Id;
        public EnemyStaticData.EnemyTypeId EnemyTypeId;
        public Vector3 SpawnPosition;
        public Vector3 SpawnRotation;
        public List<Vector3> Points;

        public EnemySpawnerData(string id, EnemyStaticData.EnemyTypeId enemyTypeId, 
            Vector3 spawnPosition, Quaternion spawnRotation, List<Transform> points)
        {
            Id = id;
            EnemyTypeId = enemyTypeId;
            SpawnPosition = spawnPosition;
            SpawnRotation = spawnRotation.eulerAngles;
            Points = new List<Vector3>();
            foreach (var point in points)
            {
                Points.Add(point.position);
            }
        }
    }
}