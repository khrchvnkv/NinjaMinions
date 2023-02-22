using System.Collections.Generic;
using NM.CoreLogic.Services.AssetManagement;
using NM.CoreLogic.Services.PersistentProgress;
using UnityEngine;

namespace NM.CoreLogic.Services.Factory
{
    public class GameFactory : IService
    {
        private const string CharactersMinion = "Characters/Minion";
        private const string HUD = "HUD/HUD";

        private readonly AssetProvider _assets;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        public GameFactory(AssetProvider assets)
        {
            _assets = assets;
        }
        public GameObject CreateHero() => InstantiateRegistered(CharactersMinion);
        public GameObject CreateHUD() => InstantiateRegistered(HUD);
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        private GameObject InstantiateRegistered(string path)
        {
            var gameObject = _assets.Instantiate(path);
            RegisterProgressListener(gameObject);
            return gameObject;
        }
        private void RegisterProgressListener(GameObject gameObject)
        {
            foreach (var reader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(reader);
            }
            
            void Register(ISavedProgressReader reader)
            {
                ProgressReaders.Add(reader);
                if (reader is ISavedProgressWriter writer)
                {
                    ProgressWriters.Add(writer);
                }
            }
        }
    }
}