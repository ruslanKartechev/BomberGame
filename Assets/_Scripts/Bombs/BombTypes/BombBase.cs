using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class BombBase : MonoBehaviour, IPlaceable
    {
        protected Coroutine _countdown;
        public string CharachterID;
        public Action OnExplode;
        public virtual void Place(Vector3 position)
        {
            transform.position = position;
        }
        public virtual void InitCoundown()
        {
            
        }


    }
}