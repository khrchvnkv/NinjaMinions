using System.Collections.Generic;
using NM.Services.AssetManagement;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.StaticData;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.Services.Factory
{
    public class GameFactory : IService
    {
        private const string MinionsMover = "Characters/MinionsMover";
        private const string CharactersMinion = "Characters/Minion";
        private const string HUD = "HUD/HUD";
        
        private const string MinionSpawner = "Characters/Minions/MinionSpawnPoint";
        private const string EnemySpawner = "Characters/Enemies/EnemySpawnPoint";

        private readonly AssetProvider _assets;
        private readonly StaticDataService _staticData;
        private readonly InputService _inputService;

        private MinionsMover _mover;

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        public GameFactory(AssetProvider assets, StaticDataService staticData, InputService inputService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
        }
        public GameObject CreateMinionsMover()
        {
            var mover = InstantiateRegistered(MinionsMover);
            _mover = mover.GetComponent<MinionsMover>();
            _mover.Construct(_inputService);
            return mover;
        }
        public GameObject CreateHUD() => InstantiateRegistered(HUD);
        public GameObject CreateMinion(Transform parent)
        {
            var minion = InstantiateRegistered(CharactersMinion, parent);
            _mover.AddMinion(minion.GetComponent<MinionMove>());
            return minion;
        }
        public void CreateMinionSpawner(Vector3 at, string spawnerId)
        {
            var spawner = InstantiateRegistered(MinionSpawner, at);
            var minionSpawner = spawner.GetComponent<MinionSpawnPoint>();
            minionSpawner.Construct(this, spawnerId);
        }
        public GameObject CreateEnemy(EnemyStaticData.EnemyTypeId enemyType, Transform parent)
        {
            var enemyData = _staticData.GetEnemyData(enemyType);
            var enemy = InstantiateRegistered(enemyData.Prefab, parent);
            return enemy;
        }
        public void CreateEnemySpawner(Vector3 at, string spawnerId, EnemyStaticData.EnemyTypeId enemyType)
        {
            var spawner = InstantiateRegistered(EnemySpawner, at);
            var enemySpawner = spawner.GetComponent<EnemySpawnPoint>();
            enemySpawner.Construct(this, spawnerId, enemyType);
        }
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }
        private GameObject InstantiateRegistered(GameObject prefab, Transform parent)
        {
            var gameObject = Object.Instantiate(prefab, parent);
            RegisterProgressListener(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string path)
        {
            var gameObject = _assets.Instantiate(path);
            RegisterProgressListener(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string path, Transform parent)
        {
            var gameObject = _assets.Instantiate(path, parent.position);
            gameObject.transform.SetParent(parent);
            RegisterProgressListener(gameObject);
            return gameObject;
        }
        private GameObject InstantiateRegistered(string path, Vector3 at)
        {
            var gameObject = _assets.Instantiate(path, at);
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