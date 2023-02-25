using System;
using System.Collections.Generic;

namespace NM.Data
{
    [Serializable]
    public class LevelState
    {
        public string Level;
        public List<HealthCharacterData> MinionsData = new List<HealthCharacterData>();
        public List<CharacterData> EnemiesData = new List<CharacterData>();

        public LevelState(string level)
        {
            Level = level;
            MinionsData.Clear();
            EnemiesData.Clear();
        }
    }
}