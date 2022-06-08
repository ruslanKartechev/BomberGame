using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "HealthBuff", menuName = "SO/Buffs/HealthBuff", order = 1)]
    public class MaxHealthBuff : ConstantAttributeBuff<int>
    {
        public override void Apply(InteractableEntity target)
        {
            try
            {
                var buffable = TryGetTarget<IMaxHealthBuffable>(target);
                buffable.SetMaxHealth(AttributeValue);
            }
            catch { }
        }

        public override void StopApply(InteractableEntity target)
        {
            try
            {
                var buffable = TryGetTarget<IMaxHealthBuffable>(target);
                buffable.RestoreMaxHealth();
            }
            catch { }
        }
    }

}
