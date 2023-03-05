using System.Collections;
using UnityEngine;

namespace ResourcesMining
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}