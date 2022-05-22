using BomberGame.UI;
using UnityEngine;
namespace BomberGame.Health
{
    public class PawnHealthWrapper : IHealth
    {
        private PawnHealth _health;
        private HealthUIChanngelSO _UIChannel;
        private DamageEffectBase _damageEffect;

        public PawnHealthWrapper(PawnHealth health, HealthUIChanngelSO healthChannel, DamageEffectBase damageEffect)
        {
            _health = health;
            _UIChannel = healthChannel;
            _damageEffect = damageEffect;
            _UIChannel?.RaiseUpdate(_health.CurrentHealth);
        }

        public void Damage(int damage)
        {
            _health?.Damage(damage);
            _UIChannel?.RaiseOnDamage();
            _UIChannel?.RaiseUpdate(_health.CurrentHealth);
            _damageEffect?.Play();
            Debug.Log("damage wrapper");
        }

        public void Heal(int heal)
        {
            _health.Heal(heal);
            _UIChannel.RaiseOnHeal();
            _UIChannel.RaiseUpdate(_health.CurrentHealth);
        }
    
    }
}