using System;
using System.Collections.Generic;

namespace NM.Data
{
    [Serializable]
    public class SaveSlotData
    {
        public string SaveTimestamp;
        public string Level;
        public List<HealthCharacterData> MinionsData = new List<HealthCharacterData>();
        public List<EnemyData> EnemiesData = new List<EnemyData>();
        public bool IsSaved;

        public SaveSlotData() { }
        public SaveSlotData(string level)
        {
            Level = level;
        }
    }
}