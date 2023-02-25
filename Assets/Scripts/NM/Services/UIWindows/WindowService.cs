using NM.Services.AssetManagement;
using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        public GameHUD GameHUD { get; private set; }

        public void RegisterHud(GameObject hud)
        {
            GameHUD = hud.GetComponent<GameHUD>();
            GameHUD.Construct();
        }
    }
}