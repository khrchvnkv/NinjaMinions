using System;
using System.Collections.Generic;
using System.Linq;
using NM.StaticData;
using UnityEngine;

namespace NM.Services.StaticData
{
    public class StaticDataService : IService
    {
        private const string LevelDataPath = "StaticData/Levels";
        private const string EnemiesDataPath = "StaticData/Enemies";
        
        private Dictionary<string, LevelStaticData> _levelData;
        private Dictionary<EnemyStaticData.EnemyTypeId, EnemyStaticData> _enemies;

        public void LoadData()
        {
            _levelData = Resources.LoadAll<LevelStaticData>(LevelDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
            _enemies = Resources.LoadAll<EnemyStaticData>(EnemiesDataPath)
                .ToDictionary(x => x.EnemyType, x => x);
        }
        public EnemyStaticData GetEnemyData(EnemyStaticData.EnemyTypeId enemyType)
        {
            if (_enemies.TryGetValue(enemyType, out var data))
            {
                return data;
            }

            throw new Exception($"No {enemyType} enemy static data");
        }
        public LevelStaticData GetLevelData(string sceneKey)
        {
            if (_levelData.TryGetValue(sceneKey, out var data))
            {
                return data;
            }

            throw new Exception($"No {sceneKey} level static data");
        }
    }
}