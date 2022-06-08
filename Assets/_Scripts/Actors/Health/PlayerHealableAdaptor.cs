using BomberGame.Health;
using BomberGame.UI;
namespace BomberGame
{
    public class PlayerHealableAdaptor : IHealable
    {
        private IHealthComponent _health;
        private bool IsHealable = true;
        private HealthUIChanngelSO _healthChannel;
        private IHealingVFX _healingVFX;
        public PlayerHealableAdaptor(IHealthComponent health, IHealingVFX healing)
        {
            _health = health;
            _healingVFX = healing;
        }

        public void Heal(int health)
        {
            if (IsHealable == false)
                return;
            _health.AddHealth(health);
            _healthChannel?.RaiseOnHeal();
            _healingVFX.PlayOnce();
        }

        public void EnableHealing()
        {
            IsHealable = true;
        }

        public void DisableHealing()
        {
            IsHealable = false;
        }
    }
}