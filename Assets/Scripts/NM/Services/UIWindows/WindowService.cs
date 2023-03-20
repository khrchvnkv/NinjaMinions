using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        private readonly IDontDestroyMarker _dontDestroyMarker;
        
        public GameObject HUD { get; private set; }
        public GameHUD GameHUD { get; private set; }
        public bool IsHudCreated => HUD != null;
        
        public WindowService(IDontDestroyMarker dontDestroyMarker)
        {
            _dontDestroyMarker = dontDestroyMarker;
        }
        public void RegisterHud(GameObject hud)
        {
            HUD = hud;
            GameHUD = hud.GetComponent<GameHUD>();
            GameHUD.Construct();
            _dontDestroyMarker.MarkAsDontDestroyable(GameHUD.gameObject);
        }
    }
}