using System.Collections;
using UnityEngine;

namespace NM.CoreLogic
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}