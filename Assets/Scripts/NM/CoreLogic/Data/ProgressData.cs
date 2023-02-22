using System;

namespace NM.CoreLogic.Data
{
    [Serializable]
    public class ProgressData
    {
        public LevelState LevelState;

        public ProgressData(string initialLevel)
        {
            LevelState = new LevelState(initialLevel);
        }
    }
}