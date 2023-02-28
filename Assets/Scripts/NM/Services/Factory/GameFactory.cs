using System;
using System.Collections.Generic;
using NM.Services.AssetManagement;
using NM.Services.Input;
using NM.Services.PersistentProgress;
using NM.Services.Pool;
using NM.Services.StaticData;
using NM.Services.UIWindows;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies;
using NM.UnityLogic.Characters.Enemies.Behaviour;
using NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman;
using NM.UnityLogic.Characters.Enemies.Behaviour.Sniper;
using NM.UnityLogic.Characters.Enemies.Behaviour.Stalker;
using NM.UnityLogic.Characters.Enemies.SpawnLogic;
using NM.UnityLogic.Characters.Minion;
using NM.UnityLogic.Characters.Minion.SpawnLogic;
using UnityEngine;

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
        private readonly PoolService _poolService;

        private MinionsMover _mover;

        private readonly List<IClearable> _clearables = new List<IClearable>();
        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgressWriter> ProgressWriters { get; } = new List<ISavedProgressWriter>();

        public event Action OnCleanedUp;

        public GameFactory(AssetProvider assets, StaticDataService staticData, InputService inputService,
            WindowService windowService, PersistentProgressService progressService, PoolService poolService)
        {
            _assets = assets;
            _staticData = staticData;
            _inputService = inputService;
            _windowService = windowService;
            _progressService = progressService;
            _poolService = poolService;
        }
        public void CreatePool(ICoroutineRunner coroutineRunner) => _poolService.CreatePool(coroutineRunner);
        public void AddToPool<T>(GameObject instance) where T : IPoolObject
        {
            _poolService.AddToPool<T>(instance);
        }
        public MinionContainer GetMinionWithId(string minionId) => _mover.GetMinionWithId(minionId);
        public GameObject CreateMinionsMover()
        {
            var instance = _poolService.GetFromPool<MinionsMover>(() => Instantiate(MinionsMover));
            RegisterProgressListener(instance);
            _mover = instance.GetComponent<MinionsMover>();
            _mover.Construct(_inputService, this, _windowService, _progressService);
            instance.SetActive(true);
            return instance;
        }
        public GameObject CreateMinion(string minionId, Transform parent)
        {
            var minion = _poolService.GetFromPool<MinionContainer>(() => 
                Instantiate(CharactersMinion));
            RegisterProgressListener(minion);
            MoveTransform(minion, parent);
            var minionStaticData = _staticData.MinionStaticData;
            var minionContainer = minion.GetComponent<MinionContainer>();
            _mover.AddMinion(minionContainer);
            minionContainer.Construct(this, minionId, minionStaticData.MaxHp, minionStaticData.MovementSpeed);
            return minion;
        }
        public void CreateMinionSpawner(MinionSpawnerData spawnerData)
        {
            var spawner = _poolService.GetFromPool<MinionSpawnPoint>(() => 
                Instantiate(MinionSpawner));
            RegisterProgressListener(spawner);
            MoveTo(spawner, spawnerData.SpawnPosition);
            var minionSpawner = spawner.GetComponent<MinionSpawnPoint>();
            minionSpawner.Construct(this, spawnerData.Id);
        }
        public GameObject CreateEnemy(EnemySpawnerData spawnerData, Transform parent)
        {
            var enemyData = _staticData.GetEnemyData(spawnerData.EnemyTypeId);
            var enemy = _poolService.GetEnemyFromPool(enemyData, () => 
                Instantiate(enemyData.Prefab, parent));
            RegisterProgressListener(enemy);
            MoveTransform(enemy, parent);
            var enemyConstruct = enemy.GetComponent<IEnemy>();
            enemyConstruct.Construct(this, spawnerData.Id, enemyData, spawnerData.Points);
            return enemy;
        }
        public void CreateEnemySpawner(EnemySpawnerData spawnerData)
        {
            var spawner = _poolService.GetFromPool<EnemySpawnPoint>(() => 
                    Instantiate(EnemySpawner));
            RegisterProgressListener(spawner);
            MoveTransform(spawner, spawnerData.SpawnPosition, spawnerData.SpawnRotation);
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
        public GameObject CreateBullet(GameObject prefab, Transform parent, BulletLogic.BulletParams bulletParams)
        {
            var bullet = _poolService.GetFromPool<BulletLogic>(() => Instantiate(prefab, parent));
            RegisterProgressListener(bullet);
            MoveTransform(bullet, parent);
            bullet.GetComponent<BulletLogic>().Construct(this, bulletParams);
            return bullet;
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
        private GameObject InstantiateRegistered(string path)
        {
            var gameObject = _assets.Instantiate(path);
            RegisterProgressListener(gameObject);
            return gameObject;
        }
        private GameObject Instantiate(string path) => _assets.Instantiate(path);
        private GameObject Instantiate(GameObject prefab, Transform parent) => _assets.Instantiate(prefab, parent);
        private void MoveTransform(GameObject movingGameObject, Transform to)
        {
            var movingTransform = movingGameObject.transform;
            movingTransform.position = to.position;
            movingTransform.rotation = to.rotation;
            movingGameObject.SetActive(true);
        }
        private void MoveTransform(GameObject movingGameObject, Vector3 toPosition, Vector3 toRotation)
        {
            var rotationQuaternion = Quaternion.Euler(toRotation);
            var movingTransform = movingGameObject.transform;
            movingTransform.position = toPosition;
            movingTransform.rotation = rotationQuaternion;
            movingGameObject.SetActive(true);
        }
        private void MoveTo(GameObject movingGameObject, Vector3 position)
        {
            movingGameObject.transform.position = position;
            movingGameObject.SetActive(true);
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