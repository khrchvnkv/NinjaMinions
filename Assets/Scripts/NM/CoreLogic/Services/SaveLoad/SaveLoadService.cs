using NM.CoreLogic.Data;
using NM.CoreLogic.Services.Factory;
using NM.CoreLogic.Services.PersistentProgress;
using UnityEngine;

namespace NM.CoreLogic.Services.SaveLoad
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
        public void SaveProgress()
        {
            foreach (var writer in _gameFactory.ProgressWriters)
            {
                writer.SaveProgress(_progressService.Progress);
            }
            PlayerPrefs.SetString(ProgressKey, _progressService.Progress.ToJson());
        }
        public ProgressData LoadProgress() => PlayerPrefs.GetString(ProgressKey)?.ToDeserialized<ProgressData>();
    }
}