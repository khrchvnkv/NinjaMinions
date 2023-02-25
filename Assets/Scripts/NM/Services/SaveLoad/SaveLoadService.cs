using System;
using System.Globalization;
using NM.Data;
using NM.Services.Factory;
using NM.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NM.Services.SaveLoad
{
    public class SaveLoadService : IService
    {
        private const string ProgressKey = "Progress";
        
        private readonly GameFactory _gameFactory;
        private readonly PersistentProgressService _progressService;

        public SaveLoadService(GameFactory gameFactory, PersistentProgressService progressService)
        {
            _gameFactory = gameFactory;
            _progressService = progressService;
        }
        public void SaveProgress(int slotIndex)
        {
            _progressService.Progress.CurrentSlotIndex = slotIndex;
            var slot = _progressService.Progress.GetSlotAt(slotIndex);
            slot.SaveTimestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            slot.Level = SceneManager.GetActiveScene().name;
            slot.MinionsData.Clear();
            slot.EnemiesData.Clear();
            slot.IsSaved = true;
            
            foreach (var writer in _gameFactory.ProgressWriters)
            {
                writer.SaveProgress(slot);
            }
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
        public ProgressData LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<ProgressData>();
    }
}