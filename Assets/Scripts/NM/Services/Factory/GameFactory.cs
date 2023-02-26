using System;
using System.Collections.Generic;
using NM.Services.AssetManagement;
using NM.Services.GameLoop;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.StaticData;
using NM.Services.UIWindows;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Enemies.Behaviour;
using NM.UnityLogic.Characters.Enemies.SpawnLogic;
using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.Characters.Minion.SpawnLogic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace NM.Services.Factory
{
    public class GameFactory : IService
    {
        private const string MinionsMover = "Characters/MinionsMover";
        private const string CharactersMinion = "Characters/Minion";
        
        private const string MinionSpawner = "Characters/Minions/MinionSpawnPoint";
        private const string EnemySpawner = "Characters/Enemies/EnemySpawnPoint";
        
        private const string HUD = "HUD/HUD";

        private readonly AssetProvider _assets;
        private readonly StaticDataService _staticData;
        private readonly InputService _inputService;
        private readonly WindowService _windowService;
        private readonly PersistentProgressService _progressService;

        private MinionsMover _mover;

        private readonly List<IClearable> _clearables = new List<IClearable>();
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        public event Action OnCleanedUp;

        public GameFactory(AssetProvider assets, StaticDataService staticData, InputService inputService, 
            WindowService windowService, PersistentProgressService progressService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
            _windowService = windowService;
            _progressService = progressService;
        }
        public GameObject CreateMinionsMover()
        {
            var mover = InstantiateRegistered(MinionsMover);
            _mover = mover.GetComponent<MinionsMover>();
            _mover.Construct(_inputService, _windowService, _progressService);
            return mover;
        }
        public GameObject CreateMinion(string minionId, Transform parent)
        {
            var minion = InstantiateRegistered(CharactersMinion, parent);
            var minionStaticData = _staticData.MinionStaticData;
            var minionContainer = minion.GetComponent<MinionContainer>();
            _mover.AddMinion(minionContainer);
            minionContainer.Construct(minionId, minionStaticData.MaxHp, minionStaticData.MovementSpeed);
            return minion;
        }
        public void CreateMinionSpawner(MinionSpawnerData spawnerData)
        {
            var at = spawnerData.SpawnPosition;
            var spawner = InstantiateRegistered(MinionSpawner, at);
            var minionSpawner = spawner.GetComponent<MinionSpawnPoint>();
            minionSpawner.Construct(this, spawnerData.Id);
        }
        public GameObject CreateEnemy(EnemySpawnerData spawnerData, Transform parent)
        {
            var enemyData = _staticData.GetEnemyData(spawnerData.EnemyTypeId);
            var enemy = InstantiateRegistered(enemyData.Prefab, parent);
            var enemyConstruct = enemy.GetComponent<IEnemy>();
            enemyConstruct.Construct(spawnerData.Id, enemyData, spawnerData.Points);
            return enemy;
        }
        public void CreateEnemySpawner(EnemySpawnerData spawnerData)
        {
            var at = spawnerData.SpawnPosition;
            var spawner = InstantiateRegistered(EnemySpawner, at);
            var enemySpawner = spawner.GetComponent<EnemySpawnPoint>();
            enemySpawner.Construct(this, spawnerData);
        }
        public GameObject CreateHud()
        {
            if (_windowService.IsHudCreated) return _windowService.GameHUD.gameObject;
            
            var hud = InstantiateRegistered(HUD);
            _windowService.RegisterHud(hud);
            return hud;
        }
        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
            foreach (var clearable in _clearables)
            {
                clearable?.Clear();
            }
            _clearables.Clear();
            OnCleanedUp?.Invoke();
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
            if (gameObject.TryGetComponent(out IClearable clearable))
            {
                _clearables.Add(clearable);
            }
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