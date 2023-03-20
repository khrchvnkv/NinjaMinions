using UnityEngine;

namespace NM
{
    public interface IDontDestroyMarker
    {
        void MarkAsDontDestroyable(GameObject gameObject);
    }
}