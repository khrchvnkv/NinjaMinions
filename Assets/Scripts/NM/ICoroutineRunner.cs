using System.Collections;
using UnityEngine;

namespace NM
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}