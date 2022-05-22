using UnityEngine;
using BomberGame.Health;
namespace BomberGame

{
    public class DamageApply
    {
        public int Amount;
        public string DealerID;
        public DamageApply(int amount, string dealerID)
        {
            Amount = amount;
            DealerID = dealerID;
        }
        public void Apply(IDamagable damagable)
        {
            damagable.TakeDamage(Amount, DealerID);
        }
    }

    public class HealApply
    {

    }

    public class BuffApply
    {

    }

    public interface IAdaptableActor
    {
        void Damage(DamageApply damage);
        void Heal(HealApply heal);
        void Buff(BuffApply buff);
    }

    public interface IAdaptableObstacle
    {
        ObstalceType GetObstacleType();
        void Damage(DamageApply damage);

    }



}
