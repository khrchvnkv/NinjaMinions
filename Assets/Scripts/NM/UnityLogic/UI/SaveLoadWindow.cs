using System.Collections.Generic;
using NM.Services.GameLoop;
using NM.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadWindowData : IWindowData
    {
        public readonly GameLoopService GameLoopService;
        public readonly PersistentProgressService ProgressService;
        
        public SaveLoadWindowData(GameLoopService gameLoopService, PersistentProgressService progressService)
        {
            GameLoopService = gameLoopService;
            ProgressService = progressService;
        }
    }
    public class SaveLoadWindow : WindowBase<SaveLoadWindowData>
    {
        [SerializeField] private List<SaveLoadSlot> _slots;
        [SerializeField] private Button _closeBtn;
        [SerializeField] private Button _restartLevelBtn;
        public override void Show(IWindowData windowData)
        {
            base.Show(windowData);
            _closeBtn.onClick.AddListener(OnCloseBtnClicked);
            _restartLevelBtn.onClick.AddListener(OnRestartBtnClicked);
            _slots.ForEach(s => s.Construct(WindowData.GameLoopService, WindowData.ProgressService, WindowService));
        }
        public override void Hide()
        {
            Time.timeScale = 1.0f;
            _closeBtn.onClick.RemoveListener(OnCloseBtnClicked);
            _restartLevelBtn.onClick.RemoveListener(OnRestartBtnClicked);
            base.Hide();
        }
        private void OnCloseBtnClicked() => WindowService.Hide<SaveLoadWindowData>();
        private void OnRestartBtnClicked()
        {
            WindowData.GameLoopService.RestartLevel();
            Hide();
        }
    }
}