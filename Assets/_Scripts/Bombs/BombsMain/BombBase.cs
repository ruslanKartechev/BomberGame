using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class BombBase : MonoBehaviour, IPlaceable
    {
        protected Coroutine _countdown;


        public virtual void Place(Vector3 position)
        {
            transform.position = position;
        }
        public virtual void InitCoundown()
        {
            
        }


    }
}