using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class CharachterColorSimple : CharachterColorBase
    {
        [SerializeField] private SpriteRenderer _rend;
        [SerializeField] private Color32 _charachterStartColor;

        private void Start()
        {
            UpdateColor();
        }
        public override void SetColor(Color32 color)
        {
            
            _charachterStartColor = color;
        
        }
        public override void UpdateColor()
        {
            if(_rend != null)
                _rend.color = _charachterStartColor;
        }
        public override Color32 GetColor()
        {
            return _charachterStartColor;
        }
    }
}