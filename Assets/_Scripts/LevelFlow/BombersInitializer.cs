using System.Collections;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System.Collections.Generic;
using System;
namespace BomberGame
{
    public abstract class BombersInitializer : MonoBehaviour
    {
        public abstract Task InitBombers(CancellationToken token);
        public abstract List<IBomberActor> InitializedBombers { get; }
        public event Action<List<IBomberActor>> OnBombersInitialized;
        protected void RaiseOnInitializer()
        {
            OnBombersInitialized?.Invoke(InitializedBombers);
        }

    }
}
