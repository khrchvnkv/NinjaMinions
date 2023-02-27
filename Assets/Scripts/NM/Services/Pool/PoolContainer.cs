using System;
using System.Collections.Generic;
using NM.Services.AssetManagement;
using UnityEngine;

namespace NM.Services.Pool
{
    public class PoolContainer
    {
        private readonly AssetProvider _assetProvider;
        private readonly Dictionary<Type, IPool> _poolCollections = new Dictionary<Type, IPool>();
        private readonly Transform _parent;

        public Transform Parent => _parent;
        public bool IsPoolCreated<T>() => _poolCollections.ContainsKey(typeof(T));

        public PoolContainer(AssetProvider assetProvider, Transform parent)
        {
            _assetProvider = assetProvider;
            _parent = parent;
        }
        public PoolCollection RegisterPool<T>(GameObject prefab) where T : IPoolObject
        {
            var pool = new PoolCollection(_assetProvider, prefab, _parent);
            _poolCollections.Add(typeof(T), pool);
            return pool;
        }
        public void AddToPool<T>(GameObject instance) where T : IPoolObject
        {
            if (_poolCollections.TryGetValue(typeof(T), out var pool))
            {
                pool.Add(instance);
            }
            else
            {
                var newPool = RegisterPool<T>(instance);
                newPool.Add(instance);
            }
        }
        public GameObject GetFromPool<T>() where T : IPoolObject
        {
            if (_poolCollections.TryGetValue(typeof(T), out var pool))
            {
                return pool.Get();
            }
            throw new Exception($"Pool with type {typeof(T)} not created");
        }
    }
}