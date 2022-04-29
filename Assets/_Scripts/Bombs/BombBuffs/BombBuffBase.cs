using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    public class BombBuffBase : ScriptableObject
    {
        public string ID;
        public virtual void Apply(GameObject target)
        {

        }
    }
}