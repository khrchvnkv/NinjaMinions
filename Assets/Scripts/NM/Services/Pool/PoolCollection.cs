using System.Collections.Generic;
using NM.Services.AssetManagement;
using UnityEngine;

namespace NM.Services.Pool
{
    public class PoolCollection : IPool
    {
        private readonly AssetProvider _provider;
        private readonly List<GameObject> _collection;
        private readonly GameObject _prefab;
        private readonly Transform _parent;

        public PoolCollection(AssetProvider provider, 
            GameObject prefab, Transform parent)
        {
            _provider = provider;
            _collection = new List<GameObject>();
            _prefab = prefab;
            _parent = parent;
        }
        public bool Contains(GameObject instance) => _collection.Contains(instance);
        public void Add(GameObject instance)
        {
            instance.SetActive(false);
            _collection.Add(instance);
        }
        public GameObject Get()
        {
            if (_collection.Count > 0)
            {
                var instance = _collection[0];
                _collection.RemoveAt(0);
                instance.SetActive(true);
                return instance;
            }

            var newItem = _provider.Instantiate(_prefab, _parent);
            newItem.transform.SetParent(_parent);
            return newItem;
        }
    }
}