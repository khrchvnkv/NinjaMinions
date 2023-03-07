using NM.Data;

namespace NM.Services.PersistentProgress
{
    public interface ISavedProgressWriter
    {
        void SaveProgress(SaveSlotData slotData);
    }
}