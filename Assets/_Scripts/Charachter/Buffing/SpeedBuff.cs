using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "SpeedBuff", menuName = "SO/Buffs/SpeedBuff_", order = 1)]

    public class SpeedBuff : BuffBase
    {
        public float SpeedMultiplier;
        public override void Apply(GameObject target)
        {
            ISpeedBuffable sb = target.GetComponent<ISpeedBuffable>();
            if(sb != null)
            {
                sb.BuffSpeed(SpeedMultiplier);
            }
        }
    }



}
