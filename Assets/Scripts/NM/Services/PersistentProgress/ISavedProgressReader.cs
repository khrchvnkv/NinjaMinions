using NM.Data;

namespace NM.Services.PersistentProgress
{
    public interface ISavedProgressReader
    {
        void LoadProgress(SaveSlotData slotData);
    }
}