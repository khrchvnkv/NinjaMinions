using UnityEngine;

namespace NM.Services.Pool
{
    internal interface IPool
    {
        bool Contains(GameObject instance);
        void Add(GameObject instance);
        GameObject Get();
    }
}