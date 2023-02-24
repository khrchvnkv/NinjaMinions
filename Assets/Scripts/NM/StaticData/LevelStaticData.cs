﻿using System.Collections.Generic;
using UnityEngine;

namespace NM.StaticData
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "StaticData/Level")]
    public class LevelStaticData : ScriptableObject
    {
        public string LevelKey;
        public List<MinionSpawnerData> MinionSpawners;
        public List<EnemySpawnerData> EnemySpawners;
    }
}