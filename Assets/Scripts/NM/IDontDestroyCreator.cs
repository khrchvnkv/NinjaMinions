using UnityEngine;

namespace NM
{
    public interface IDontDestroyCreator
    {
        void MarkAsDontDestroyOnLoad(GameObject gameObject);
    }
}