using System;
using NM.Services.AssetManagement;
using NM.StaticData;
using NM.UnityLogic.Characters.Enemies.Behaviour.Patrolman;
using NM.UnityLogic.Characters.Enemies.Behaviour.Sniper;
using NM.UnityLogic.Characters.Enemies.Behaviour.Stalker;
using UnityEngine;

namespace NM.Services.Pool
{
    public class PoolService : IService
    {
        private readonly AssetProvider _assetProvider;
        
        private PoolContainer _pool;
        
        public delegate GameObject CreateInstanceOperation();
        
        public PoolService(AssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        public void CreatePool(IDontDestroyMarker dontDestroyMarker)
        {
            if (_pool == null)
            {
                var pool = new GameObject("Pool");
                dontDestroyMarker.MarkAsDontDestroyable(pool);
                _pool = new PoolContainer(_assetProvider, pool.transform);
            }
        }
        public void AddToPool<T>(GameObject instance) where T : IPoolObject
        {
            _pool.AddToPool<T>(instance);
        }
        public GameObject GetFromPool<T>(CreateInstanceOperation operation) where T : IPoolObject
        {
            if (!_pool.IsPoolCreated<T>())
            {
                var instance = operation();
                instance.transform.SetParent(_pool.Parent);
                _pool.RegisterPool<T>(instance);
                return instance;
            }
            var itemFromPool = _pool.GetFromPool<T>();
            return itemFromPool;
        }
        public GameObject GetEnemyFromPool(EnemyStaticData staticData, CreateInstanceOperation createOperation)
        {
            switch (staticData.EnemyType)
            {
                case EnemyStaticData.EnemyTypeId.Patrolman:
                    return GetFromPool<PatrolmanEnemy>(createOperation);
                case EnemyStaticData.EnemyTypeId.Stalker:
                    return GetFromPool<StalkerEnemy>(createOperation);
                case EnemyStaticData.EnemyTypeId.Sniper:
                    return GetFromPool<SniperEnemy>(createOperation);
            }
            throw new Exception($"Unknown type {staticData.EnemyType}");
        }
    }
}