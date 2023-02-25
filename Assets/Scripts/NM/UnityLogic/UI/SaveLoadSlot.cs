using NM.Services;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.States;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadSlot : MonoBehaviour
    {
        [SerializeField] private Button _saveBtn;
        [SerializeField] private Button _loadBtn;

        private GameStateMachine _gameStateMachine;
        private PersistentProgressService _progressService;
        private InputService _inputService;
        private SaveLoadService _saveLoadService;
        private GameHUD _gameHUD;

        public void Construct(GameStateMachine gameStateMachine, PersistentProgressService progressService,
            InputService inputService, SaveLoadService saveLoadService, GameHUD gameHUD)
        {
            _gameStateMachine = gameStateMachine;
            _progressService = progressService;
            _inputService = inputService;
            _saveLoadService = saveLoadService;
            _gameHUD = gameHUD;
        }
        private void OnEnable()
        {
            _saveBtn.onClick.AddListener(SaveProgress);
            _loadBtn.onClick.AddListener(LoadProgress);
        }
        private void OnDisable()
        {
            _saveBtn.onClick.RemoveListener(SaveProgress);
            _loadBtn.onClick.RemoveListener(LoadProgress);
        }
        private void SaveProgress() => AllServices.Container.Single<SaveLoadService>().SaveProgress();
        private void LoadProgress()
        {
            _inputService.Deactivate();
            _gameHUD.Hide<SaveLoadWindowData>();
            _progressService.Progress = _saveLoadService.LoadProgress();
            _gameStateMachine.Enter<LoadLevelState>(_progressService.Progress.LevelState.Level);
        }
    }
}