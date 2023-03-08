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
        public SaveSlotBuilder With() => new SaveSlotBuilder(this);

        public class SaveSlotBuilder
        {
            private readonly SaveSlotData _slotData;
            public SaveSlotBuilder(SaveSlotData slotData)
            {
                _slotData = slotData;
            }
            public SaveSlotBuilder WithSaveTimeStamp(string timestampKey)
            {
                _slotData.SaveTimestamp = timestampKey;
                return this;
            }
            public SaveSlotBuilder WithLevel(string levelId)
            {
                _slotData.Level = levelId;
                return this;
            }
            public SaveSlotBuilder WithEmptyCharactersData()
            {
                _slotData.MinionsData.Clear();
                _slotData.EnemiesData.Clear();
                return this;
            }
            public SaveSlotBuilder WithSavedStatus(bool isSaved)
            {
                _slotData.IsSaved = isSaved;
                return this;
            }
        }
    }
}