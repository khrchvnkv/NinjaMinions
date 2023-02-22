using NM.CoreLogic.Data;

namespace NM.CoreLogic.Services.PersistentProgress
{
    public interface ISavedProgressReader : ISavedProgressWriter
    {
        void LoadProgress(ProgressData progress);
    }
}