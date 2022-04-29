using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class BombColorBase : MonoBehaviour
    {
        //[SerializeField] private SpriteRenderer _rend;
        public abstract void SetColor(Color32 color);
        
    }
}