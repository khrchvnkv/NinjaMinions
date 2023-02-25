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
        [SerializeField] private int _slotIndex;
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
            AllServices.Container.Single<SaveLoadService>().SaveProgress(_slotIndex);
            UpdateSlotInfoText();
        }
        private void LoadProgress()
        {
            if (CanReloadProgress())
            {
                _gameHUD.Hide<SaveLoadWindowData>();
                _gameLoopService.ReloadProgress(_slotIndex);
            }
        }
        private bool CanReloadProgress()
        {
            var slot = _progressService.Progress.GetSlotAt(_slotIndex);
            return slot.IsSaved;
        }
        private void UpdateSlotInfoText()
        {
            var slot = _progressService.Progress.GetSlotAt(_slotIndex);
            var saveTimestamp = slot.SaveTimestamp;
            if (!string.IsNullOrEmpty(saveTimestamp))
            {
                _slotInfoText.text = $"{saveTimestamp}";
            }
        }
    }
}