using System;
using System.Collections.Generic;

namespace NM.CoreLogic.Data
{
    [Serializable]
    public class LevelState
    {
        public string Level;
        public List<MinionsData> MinionsData = new List<MinionsData>();

        public LevelState(string level)
        {
            Level = level;
            MinionsData.Clear();
        }
    }
}