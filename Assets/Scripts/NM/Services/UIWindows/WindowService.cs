using NM.UnityLogic.UI;
using UnityEngine;

namespace NM.Services.UIWindows
{
    public class WindowService : IService
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        public GameHUD GameHUD { get; private set; }
        public bool IsHudCreated { get; private set; }
        
        public WindowService(ICoroutineRunner coroutineRunner)
        {
            _coroutineRunner = coroutineRunner;
        }
        public void RegisterHud(GameObject hud)
        {
            GameHUD = hud.GetComponent<GameHUD>();
            GameHUD.Construct();
            _coroutineRunner.MarkAsDontDestroyOnLoad(GameHUD);
            IsHudCreated = true;
        }
    }
}