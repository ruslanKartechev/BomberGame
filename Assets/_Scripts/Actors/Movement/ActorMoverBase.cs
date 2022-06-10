
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public abstract class ActorMoverBase<T> 
    {
        public event Action<T> OnPositionChange;
        public abstract Task ModeToDir(T direction, CancellationToken token);
        protected void RaiseOnPositionChange(T value)
        {
            OnPositionChange?.Invoke(value);
        }
    }
}