using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        private readonly IDontDestroyCreator _dontDestroyCreator;
        
        public GameHUD GameHUD { get; private set; }
        public bool IsHudCreated { get; private set; }
        
        public WindowService(IDontDestroyCreator dontDestroyCreator)
        {
            _dontDestroyCreator = dontDestroyCreator;
        }
        public void RegisterHud(GameObject hud)
        {
            GameHUD = hud.GetComponent<GameHUD>();
            GameHUD.Construct();
            _dontDestroyCreator.MarkAsDontDestroyOnLoad(GameHUD.gameObject);
            IsHudCreated = true;
        }
    }
}