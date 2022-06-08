using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class BombColorSimple : BombColorBase
    {
        [SerializeField] private SpriteRenderer _rend;

        public override void SetColor(Color32 color)
        {
            if (_rend != null)
                _rend.color = color;
        }
    }
}