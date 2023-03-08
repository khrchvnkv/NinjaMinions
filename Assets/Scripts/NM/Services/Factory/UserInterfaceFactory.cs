using NM.Services.UIWindows;
using UnityEngine;

namespace NM.Services.Factory
{
    internal class UserInterfaceFactory
    {
        private const string HUD = "HUD/HUD";

        private readonly AssetFactory _assetFactory;
        private readonly WindowService _windowService;

        public UserInterfaceFactory(AssetFactory assetFactory, WindowService windowService)
        {
            _assetFactory = assetFactory;
            _windowService = windowService;
        }
        internal GameObject CreateHud()
        {
            if (_windowService.IsHudCreated)
            {
                _assetFactory.RegisterProgressListener(_windowService.HUD);
                return _windowService.HUD;
            }
            else
            {
                var hud = _assetFactory.InstantiateAsSingle(HUD);
                _windowService.RegisterHud(hud);
                return hud;
            }
        }
    }
}