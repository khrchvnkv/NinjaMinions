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
using NM.UnityLogic.Characters.Minion;
using UnityEngine;

namespace NM.Services.Factory
{
    public class GameFactory : IService
    {
        private readonly PoolService _poolService;

        private readonly SavedProgressRegister _savedProgressRegister;
        private readonly AssetFactory _assetFactory;
        private readonly UserInterfaceFactory _userInterfaceFactory;
        private readonly MinionsFactory _minionsFactory;
        private readonly EnemiesFactory _enemiesFactory;
        
        public event Action OnCleanedUp;

        public GameFactory(AssetProvider assets, StaticDataService staticData, InputService inputService,
            WindowService windowService, PersistentProgressService progressService, PoolService poolService)
        {
            _poolService = poolService;

            #region Initialize Derived Factories

            _savedProgressRegister = new SavedProgressRegister();
            _assetFactory = new AssetFactory(assets, _savedProgressRegister, poolService);
            _userInterfaceFactory = new UserInterfaceFactory(_assetFactory, windowService);
            _minionsFactory = new MinionsFactory(_assetFactory, inputService, progressService, staticData, 
                windowService, this);
            _enemiesFactory = new EnemiesFactory(_assetFactory, this, staticData);

            #endregion
        }
        public void CreatePool(IDontDestroyCreator dontDestroyCreator) => _poolService.CreatePool(dontDestroyCreator);
        public void AddToPool<T>(GameObject instance) where T : IPoolObject => _poolService.AddToPool<T>(instance);
        public GameObject CreateHud() => _userInterfaceFactory.CreateHud();
        public MinionContainer GetMinionWithId(string minionId) => _minionsFactory.GetMinionWithId(minionId);
        public GameObject CreateMinionsMover() => _minionsFactory.CreateMinionsMover();
        public GameObject CreateMinion(string minionId, Transform parent) =>
            _minionsFactory.CreateMinion(minionId, parent);
        public void CreateMinionSpawner(MinionSpawnerData spawnerData) =>
            _minionsFactory.CreateMinionSpawner(spawnerData);
        public GameObject CreateEnemy(EnemySpawnerData spawnerData, Transform parent) =>
            _enemiesFactory.CreateEnemy(spawnerData, parent);
        public void CreateEnemySpawner(EnemySpawnerData spawnerData) => _enemiesFactory.CreateEnemySpawner(spawnerData);
        public GameObject CreateBullet(GameObject prefab, Transform parent, BulletLogic.BulletParams bulletParams) =>
            _enemiesFactory.CreateBullet(prefab, parent, bulletParams);
        public IEnumerable<ISavedProgressReader> GetProgressReaders() => _savedProgressRegister.ProgressReaders;
        public IEnumerable<ISavedProgressWriter> GetProgressWriters() => _savedProgressRegister.ProgressWriters;
        public void Cleanup()
        {
            _savedProgressRegister.Cleanup();
            OnCleanedUp?.Invoke();
        }
        internal static void MoveTransform(GameObject movingGameObject, Transform to)
        {
            var movingTransform = movingGameObject.transform;
            movingTransform.position = to.position;
            movingTransform.rotation = to.rotation;
            movingGameObject.SetActive(true);
        }
        internal static void MoveTransform(GameObject movingGameObject, Vector3 toPosition, Vector3 toRotation)
        {
            var rotationQuaternion = Quaternion.Euler(toRotation);
            var movingTransform = movingGameObject.transform;
            movingTransform.position = toPosition;
            movingTransform.rotation = rotationQuaternion;
            movingGameObject.SetActive(true);
        }
        internal static void MoveTo(GameObject movingGameObject, Vector3 position)
        {
            movingGameObject.transform.position = position;
            movingGameObject.SetActive(true);
        }
    }
}