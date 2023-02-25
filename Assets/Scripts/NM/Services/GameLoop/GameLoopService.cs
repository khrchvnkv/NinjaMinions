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
        public bool CanReloadProgress() => _saveLoadService.HasSavedData();
        public void ReloadProgress()
        {
            _inputService.Deactivate();
            _persistentProgressService.Progress = _saveLoadService.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState>(_persistentProgressService.Progress.LevelState.Level);
        }
        public void RestartLevel()
        {
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
            _persistentProgressService.Progress.LevelState =
                new LevelState(sceneKey);
            _inputService.Deactivate();
            _gameStateMachine.Enter<LoadLevelState>(sceneKey);
        }
    }
}