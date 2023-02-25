using NM.Services.GameLoop;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;
using UnityEngine.UI;

namespace NM.UnityLogic.UI
{
    public class HudWindowData : IWindowData
    {
        public readonly GameLoopService GameLoopService;
        public readonly IHealthCharacter HealthCharacter;

        public HudWindowData(GameLoopService gameLoopService, IHealthCharacter character)
        {
            GameLoopService = gameLoopService;
            HealthCharacter = character;
        }
    }
    public class HudWindow : WindowBase<HudWindowData>
    {
        [SerializeField] private Slider _hpSlider;
        [SerializeField] private Button _settingsWindowBtn;

        public override void Show(IWindowData windowData)
        {
            base.Show(windowData);
            WindowData.HealthCharacter.OnHpChanged += ChangeHp;
            SetMaxHp(WindowData.HealthCharacter.MaxHp);
            ChangeHp(WindowData.HealthCharacter.HP);
            _settingsWindowBtn.onClick.AddListener(OpenSaveLoadSettings);
        }
        public override void Hide()
        {
            if (WindowData != null)
            {
                WindowData.HealthCharacter.OnHpChanged -= ChangeHp;
            }
            _settingsWindowBtn.onClick.RemoveListener(OpenSaveLoadSettings);
            base.Hide();
        }
        private void OpenSaveLoadSettings()
        {
            Time.timeScale = 0.0f;
            GameHUD.Show(new SaveLoadWindowData(WindowData.GameLoopService));
        }
        private void SetMaxHp(int maxHp) => _hpSlider.maxValue = maxHp;
        private void ChangeHp(int newHpValue) => _hpSlider.value = newHpValue;
    }
}