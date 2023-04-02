using NM.Services;
using NM.Services.GameLoop;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.Services.UIWindows;
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
        private WindowService _windowService;

        public void Construct(GameLoopService gameLoopService, PersistentProgressService progressService, WindowService windowService)
        {
            _gameLoopService = gameLoopService;
            _progressService = progressService;
            _windowService = windowService;
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
                _windowService.Hide<SaveLoadWindowData>();
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