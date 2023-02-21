using NM.CoreLogic.Data;

namespace NM.CoreLogic.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(ProgressData progress);
    }
}