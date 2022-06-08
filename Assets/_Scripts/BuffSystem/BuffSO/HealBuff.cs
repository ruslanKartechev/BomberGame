using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "HealBuff", menuName = "SO/Buffs/HealBuff", order = 1)]
    public class HealBuff : ConstantAttributeBuff<int>
    {
        public override void Apply(InteractableEntity target)
        {
            try
            {
                var buffable = TryGetTarget<IHealable>(target);
                buffable.Heal(AttributeValue);
            }
            catch { }
        }


        public override void StopApply(InteractableEntity target)
        {
        }
    }

}
