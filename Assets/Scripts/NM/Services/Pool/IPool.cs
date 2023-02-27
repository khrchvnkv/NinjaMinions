using UnityEngine;

namespace NM.Services.Pool
{
    internal interface IPool
    {
        void Add(GameObject instance);
        GameObject Get();
    }
}