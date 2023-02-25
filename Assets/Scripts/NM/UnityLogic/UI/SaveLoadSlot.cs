using NM.Services;
using NM.Services.GameLoop;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadSlot : MonoBehaviour
    {
        [SerializeField] private TMP_Text _slotInfoText;
        [SerializeField] private Button _saveBtn;
        [SerializeField] private Button _loadBtn;

        private GameLoopService _gameLoopService;
        private PersistentProgressService _progressService;
        private GameHUD _gameHUD;

        public void Construct(GameLoopService gameLoopService, PersistentProgressService progressService, GameHUD gameHUD)
        {
            _gameLoopService = gameLoopService;
            _progressService = progressService;
            _gameHUD = gameHUD;
            UpdateSlotInfoText();
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
        private void SaveProgress()
        {
            AllServices.Container.Single<SaveLoadService>().SaveProgress();
            UpdateSlotInfoText();
        }
        private void LoadProgress()
        {
            if (_gameLoopService.CanReloadProgress())
            {
                _gameHUD.Hide<SaveLoadWindowData>();
                _gameLoopService.ReloadProgress();
            }
        }
        private void UpdateSlotInfoText()
        {
            var saveTime = _progressService.Progress.LevelState.SaveTimestamp;
            if (saveTime != default)
            {
                _slotInfoText.text = $"{saveTime}";
            }
        }
    }
}