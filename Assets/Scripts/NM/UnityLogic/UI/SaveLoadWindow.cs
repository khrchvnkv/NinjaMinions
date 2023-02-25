using System.Collections.Generic;
using NM.Services.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadWindowData : IWindowData
    {
        public readonly GameLoopService GameLoopService;
        
        public SaveLoadWindowData(GameLoopService gameLoopService)
        {
            GameLoopService = gameLoopService;
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
            _slots.ForEach(s => s.Construct(WindowData.GameLoopService, GameHUD));
        }
        public override void Hide()
        {
            Time.timeScale = 1.0f;
            _closeBtn.onClick.RemoveListener(OnCloseBtnClicked);
            _restartLevelBtn.onClick.RemoveListener(OnRestartBtnClicked);
            base.Hide();
        }
        private void OnCloseBtnClicked() => GameHUD.Hide<SaveLoadWindowData>();
        private void OnRestartBtnClicked()
        {
            WindowData.GameLoopService.RestartLevel();
            Hide();
        }
    }
}