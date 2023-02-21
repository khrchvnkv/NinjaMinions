using UnityEngine;

namespace NM.CoreLogic.Services.AssetManagement
{
    public class AssetProvider : IService
    {
        public GameObject Instantiate(string path)
        {
            var prefab = Resources.Load<GameObject>(path);
            return Object.Instantiate(prefab);
        }
    }
}