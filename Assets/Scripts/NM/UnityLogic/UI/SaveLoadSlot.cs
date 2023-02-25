using NM.Services;
using NM.Services.GameLoop;
using NM.Services.SaveLoad;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadSlot : MonoBehaviour
    {
        [SerializeField] private Button _saveBtn;
        [SerializeField] private Button _loadBtn;

        private GameLoopService _gameLoopService;
        private GameHUD _gameHUD;

        public void Construct(GameLoopService gameLoopService, GameHUD gameHUD)
        {
            _gameLoopService = gameLoopService;
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
            if (_gameLoopService.CanReloadProgress())
            {
                _gameHUD.Hide<SaveLoadWindowData>();
                _gameLoopService.ReloadProgress();
            }
        }
    }
}