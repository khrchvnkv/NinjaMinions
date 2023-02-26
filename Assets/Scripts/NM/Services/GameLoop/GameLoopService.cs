using NM.Data;
using NM.Services.Input;
using NM.Services.PersistentProgress;
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
        private readonly StaticDataService _staticDataService;

        public GameLoopService(GameStateMachine gameStateMachine, PersistentProgressService persistentProgressService,
            InputService inputService, StaticDataService staticDataService)
        {
            _gameStateMachine = gameStateMachine;
            _persistentProgressService = persistentProgressService;
            _inputService = inputService;
            _staticDataService = staticDataService;
        }
        public void ReloadProgress(int slotIndex)
        {
            _inputService.Deactivate();
            _persistentProgressService.Progress.CurrentSlotIndex = slotIndex;
            var slot = _persistentProgressService.Progress.CurrentSlot;
            _gameStateMachine.Enter<LoadLevelState>(slot);
        }
        public void RestartLevel()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var slot = new SaveSlotData(currentScene);
            LoadSlot(slot);
        }
        public void CompleteLevel()
        {
            var currentScene = SceneManager.GetActiveScene().name;
            var nextSceneKey = _staticDataService.GetNextLevelSceneKey(currentScene);
            var slot = new SaveSlotData(nextSceneKey);
            LoadSlot(slot);
        }
        private void LoadSlot(SaveSlotData slot)
        {
            _inputService.Deactivate();
            _gameStateMachine.Enter<LoadLevelState>(slot);
        }
    }
}