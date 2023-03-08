using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        private readonly IDontDestroyCreator _dontDestroyCreator;
        
        public GameObject HUD { get; private set; }
        public GameHUD GameHUD { get; private set; }
        public bool IsHudCreated => HUD != null;
        
        public WindowService(IDontDestroyCreator dontDestroyCreator)
        {
            _dontDestroyCreator = dontDestroyCreator;
        }
        public void RegisterHud(GameObject hud)
        {
            HUD = hud;
            GameHUD = hud.GetComponent<GameHUD>();
            GameHUD.Construct();
            _dontDestroyCreator.MarkAsDontDestroyOnLoad(GameHUD.gameObject);
        }
    }
}