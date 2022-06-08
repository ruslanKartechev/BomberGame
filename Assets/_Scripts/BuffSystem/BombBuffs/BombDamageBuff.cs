
using UnityEngine;
namespace BomberGame.Bombs
{
    [CreateAssetMenu(fileName = "BombDamageBuff", menuName = "SO/Buffs/BombDamageBuff", order = 1)]
    public class BombDamageBuff : BombBuffBase
    {
        [SerializeField] private int Damage;

        public override void ApplyToBomb(InteractableEntity bomb)
        {
            try
            {
                var buffable = bomb.GetEntityComponent<IDamageBuffable>();
                buffable.BuffDamage(Damage);
            } catch
            {
                Debug.Log("Cannot apply damage buff");
            }
           
        }

        public override string GetStringValue()
        {
            return Damage.ToString();
        }
    }
}
