using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        private readonly IDontDestroyMarker _dontDestroyMarker;
        
        public GameObject HUD { get; private set; }
        public bool IsHudCreated => HUD != null;
        
        private GameHUD _gameHUD;

        public WindowService(IDontDestroyMarker dontDestroyMarker)
        {
            _dontDestroyMarker = dontDestroyMarker;
        }
        public void RegisterHud(GameObject hud)
        {
            HUD = hud;
            _gameHUD = hud.GetComponent<GameHUD>();
            _gameHUD.Construct(this);
            _dontDestroyMarker.MarkAsDontDestroyable(_gameHUD.gameObject);
        }
        public void Show<TData>(TData data) where TData : IWindowData => _gameHUD.Show(data);
        public void Hide<TData>() where TData : IWindowData => _gameHUD.Hide<TData>();
    }
}