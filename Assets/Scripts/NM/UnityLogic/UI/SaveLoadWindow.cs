using System.Collections.Generic;
using NM.Data;
using NM.Services;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.SaveLoad;
using NM.States;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class SaveLoadWindowData : IWindowData
    {
        public readonly GameStateMachine GameStateMachine;
        public readonly InputService InputService;
        public readonly PersistentProgressService PersistentProgressService;
        public readonly SaveLoadService SaveLoadService;

        public SaveLoadWindowData(GameStateMachine gameStateMachine, InputService inputService,
            PersistentProgressService persistentProgressService)
        {
            GameStateMachine = gameStateMachine;
            InputService = inputService;
            PersistentProgressService = persistentProgressService;
            SaveLoadService = AllServices.Container.Single<SaveLoadService>();
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
            _slots.ForEach(s => s.Construct(WindowData.GameStateMachine, WindowData.PersistentProgressService, 
                WindowData.InputService, WindowData.SaveLoadService, GameHUD));
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
            var currentScene = SceneManager.GetActiveScene().name;
            WindowData.PersistentProgressService.Progress.LevelState =
                new LevelState(currentScene);
            WindowData.InputService.Deactivate();
            WindowData.GameStateMachine.Enter<LoadLevelState>(currentScene);
            Hide();
        }
    }
}