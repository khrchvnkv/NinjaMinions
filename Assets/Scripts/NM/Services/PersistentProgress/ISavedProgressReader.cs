using NM.Data;

namespace NM.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(ProgressData progress);
    }
}