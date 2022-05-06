using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class EnemyMover : MonoBehaviour
    {
        public Vector3 PrevPosition { get; protected set; }
        public Vector3 CurrentDir { get; protected set; }
        public event Notifier DirectionChange;
        public event Notifier MoveMade;
        protected float _moveTime = 1;
        protected void RaiseDirChange()
        {
            DirectionChange?.Invoke();
        }
        protected void RaiseMoveMade()
        {
            MoveMade?.Invoke();
        }

        public virtual void Init(float moveTime)
        {
            _moveTime = moveTime;
        }

        public virtual void Enable()
        {

        }
        public virtual void Disable()
        {

        }

    }
}