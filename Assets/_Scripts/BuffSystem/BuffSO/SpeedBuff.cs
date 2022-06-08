using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "SpeedBuff", menuName = "SO/Buffs/SpeedBuff", order = 1)]
    public class SpeedBuff : ConstantAttributeBuff<float>
    {
        public override void Apply(InteractableEntity target)
        {
            try
            {
                var buffable = TryGetTarget<ISpeedBuffable>(target);
                buffable.BuffSpeed(AttributeValue);
            }
            catch { }
        }

        public override void StopApply(InteractableEntity target)
        {
            try
            {
                var buffable = TryGetTarget<ISpeedBuffable>(target);
                buffable.RestoreOriginal();
            }
            catch { }
        }
    }




}
