using System;
using System.Collections.Generic;

namespace NM.Data
{
    [Serializable]
    public class ProgressData
    {
        public int CurrentSlotIndex;
        public List<SaveSlotData> Slots = new List<SaveSlotData>();

        public SaveSlotData CurrentSlot => Slots[CurrentSlotIndex];
        
        public ProgressData(string initialLevel)
        {
            CurrentSlotIndex = 0;
            Slots.Add(new SaveSlotData(initialLevel));
        }
        public SaveSlotData GetSlotAt(int slotIndex)
        {
            var neededSlotsCount = slotIndex + 1;
            if (neededSlotsCount > Slots.Count)
            {
                CreateSlotsPack(neededSlotsCount);
            }
            return Slots[slotIndex];
        }
        public void SetCurrentSlot(SaveSlotData slot) => Slots[CurrentSlotIndex] = slot;
        private void CreateSlotsPack(int slotsCount)
        {
            while (Slots.Count < slotsCount)
            {
                Slots.Add(new SaveSlotData());
            }
        }
    }
}