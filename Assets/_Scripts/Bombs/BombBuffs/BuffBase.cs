using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    public class BuffBase : ScriptableObject
    {
        public string ID;
        public virtual void Apply(GameObject target)
        {

        }
    }
}