using NM.Data;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.Services.StaticData;
using NM.States;
using UnityEngine.SceneManagement;

namespace NM.Services.GameLoop
{
    public class GameLoopService : IService
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly PersistentProgressService _persistentProgressService;
        private readonly InputService _inputService;
        private readonly SaveLoadService _saveLoadService;
        private readonly StaticDataService _staticDataService;

        public GameLoopService(GameStateMachine gameStateMachine, PersistentProgressService persistentProgressService,
            InputService inputService, SaveLoadService saveLoadService, StaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }
        public void ReloadProgress(int slotIndex)
        {
            _inputService.Deactivate();
            var level = _persistentProgressService.Progress.GetSlotAt(slotIndex).Level;
            _persistentProgressService.Progress = _saveLoadService.LoadProgress();
            _persistentProgressService.Progress.CurrentSlotIndex = slotIndex;
            _gameStateMachine.Enter<LoadLevelState>(level);
        }
        public void RestartLevel()
        {
            var slot = _persistentProgressService.Progress.CurrentSlot;
            slot.MinionsData.Clear();
            slot.EnemiesData.Clear();
            var currentScene = SceneManager.GetActiveScene().name;
            LoadScene(currentScene);
        }
        public void CompleteLevel()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var nextSceneKey = _staticDataService.GetNextLevelSceneKey(currentScene);
            LoadScene(nextSceneKey);
        }
        private void LoadScene(string sceneKey)
        {
            _inputService.Deactivate();
            _gameStateMachine.Enter<LoadLevelState>(sceneKey);
        }
    }
}