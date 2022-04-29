using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombBuff_Length", menuName = "SO/Buffs/BombBuff_Length", order = 1)]
    public class BombBuff_Length : BombBuffBase
    {
        public int NewExplosionLength;
        public override void Apply(GameObject target)
        {
            ILengthBuffable lb = target.GetComponent<ILengthBuffable>();
            if(lb != null)
                lb.BuffLength(NewExplosionLength);
        }
    }
}