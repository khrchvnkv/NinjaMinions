using System;

namespace NM.Data
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