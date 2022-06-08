using UnityEngine;
using System.Collections;
namespace CommonGame
{
    public interface ICoroutineRunner
    {
        Coroutine StartRoutine(IEnumerator enumerator);
        void StopRoutine(Coroutine routine);
    }
}
