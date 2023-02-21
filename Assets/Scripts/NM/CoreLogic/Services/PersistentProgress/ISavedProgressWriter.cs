using NM.CoreLogic.Data;

namespace NM.CoreLogic.Services.PersistentProgress
{
    public interface ISavedProgressWriter
    {
        void SaveProgress(ProgressData progress);
    }
}