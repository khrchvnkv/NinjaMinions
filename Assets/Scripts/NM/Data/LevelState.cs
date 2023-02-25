using System;
using System.Collections.Generic;
using System.Globalization;

namespace NM.Data
{
    [Serializable]
    public class LevelState
    {
        public string SaveTimestamp;
        public string Level;
        public List<HealthCharacterData> MinionsData = new List<HealthCharacterData>();
        public List<CharacterData> EnemiesData = new List<CharacterData>();

        public LevelState(string level)
        {
            Level = level;
            MinionsData.Clear();
            EnemiesData.Clear();
        }
        public LevelState(string level, string savedTimestamp)
        {
            SaveTimestamp = savedTimestamp;
            Level = level;
            MinionsData.Clear();
            EnemiesData.Clear();
        }
    }
}