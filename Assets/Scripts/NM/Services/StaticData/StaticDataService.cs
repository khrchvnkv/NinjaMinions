﻿using System;
using System.Collections.Generic;
using System.Linq;
using NM.StaticData;
using UnityEngine;

namespace NM.Services.StaticData
{
    public class StaticDataService : IService
    {
        private const string LevelDataPath = "StaticData/Levels";
        private const string MinionDataPath = "StaticData/Minions/MinionData";
        private const string EnemiesDataPath = "StaticData/Enemies";

        private Dictionary<string, LevelStaticData> _levelData;
        private Dictionary<EnemyStaticData.EnemyTypeId, EnemyStaticData> _enemies;

        public MinionStaticData MinionStaticData { get; private set; }
        
        public void LoadData()
        {
            _levelData = Resources.LoadAll<LevelStaticData>(LevelDataPath)
                .ToDictionary(x => x.LevelKey, x => x);
            MinionStaticData = Resources.Load<MinionStaticData>(MinionDataPath);
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
        public string GetNextLevelSceneKey(string currentSceneKey)
        {
            var levelDataCollection = _levelData.Values.ToArray();
            var currentSceneIndex = 0;
            for (int i = 0; i < levelDataCollection.Length; i++)
            {
                if (levelDataCollection[i].LevelKey == currentSceneKey)
                {
                    currentSceneIndex = i;
                    break;
                }
            }
            var nextSceneIndex = currentSceneIndex + 1;
            if (nextSceneIndex >= levelDataCollection.Length)
            {
                nextSceneIndex = 0;
            }

            return levelDataCollection[nextSceneIndex].LevelKey;
        }
    }
}