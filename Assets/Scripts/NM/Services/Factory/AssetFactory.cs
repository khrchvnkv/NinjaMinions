using NM.Services.AssetManagement;
using NM.Services.Pool;
using NM.StaticData;
using UnityEngine;

namespace NM.Services.Factory
{
    public class AssetFactory
    {
        private readonly AssetProvider _assets;
        private readonly SavedProgressRegister _savedProgressRegister;
        private readonly PoolService _poolService;

        public AssetFactory(AssetProvider assets, SavedProgressRegister savedProgressRegister, PoolService poolService)
        {
            _assets = assets;
            _savedProgressRegister = savedProgressRegister;
            _poolService = poolService;
        }
        public GameObject InstantiateAsSingle(string path)
        {
            var instance = Instantiate(path);
            RegisterProgressListener(instance);
            return instance;
        }
        public GameObject CreateAssetByName<T>(string path) where T : IPoolObject
        {
            var instance = _poolService.GetFromPool<T>(() => Instantiate(path));
            RegisterProgressListener(instance);
            return instance;
        }
        public GameObject CreateAssetByInstance<T>(GameObject prefab, Transform parent) where T : IPoolObject
        {
            var instance = _poolService.GetFromPool<T>(() => Instantiate(prefab, parent));
            RegisterProgressListener(instance);
            return instance;
        }
        public GameObject CreateEnemyAssetByData(EnemyStaticData enemyData, Transform parent)
        {
            var instance = _poolService.GetEnemyFromPool(enemyData, () => 
                Instantiate(enemyData.Prefab, parent));
            RegisterProgressListener(instance);
            return instance;
        }
        public void RegisterProgressListener(GameObject gameObject) =>
            _savedProgressRegister.RegisterProgressListener(gameObject);
        private GameObject Instantiate(string path) => _assets.Instantiate(path);
        private GameObject Instantiate(GameObject prefab, Transform parent) => _assets.Instantiate(prefab, parent);
    }
}