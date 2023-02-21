using NM.CoreLogic.Services.AssetManagement;
using UnityEngine;

namespace NM.CoreLogic.Services.Factory
{
    public class GameFactory : IService
    {
        private const string CharactersMinion = "Characters/Minion";
        private const string HUD = "HUD/HUD";

        private readonly AssetProvider _assets;

        public GameFactory(AssetProvider assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero() => _assets.Instantiate(CharactersMinion);
        public GameObject CreateHUD() => _assets.Instantiate(HUD);
    }
}