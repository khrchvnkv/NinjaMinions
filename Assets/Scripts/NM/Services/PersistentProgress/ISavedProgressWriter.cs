﻿using NM.Data;

namespace NM.Services.PersistentProgress
{
    public interface ISavedProgressWriter : ISavedProgressReader
    {
        void SaveProgress(ProgressData progress);
    }
}