using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public class LineExplosion : ExplosionEffect
    {

        [SerializeField] private BombLineEffectSO _lineEffect;
        public override void Play(Vector2 center, List<ExplosionPositions> results)
        {
            foreach(ExplosionPositions res in results)
            {
                _lineEffect.PlayLineEffect(center, res.Direction, res.Length,Duration);
            }
            
        }

        

    }
}