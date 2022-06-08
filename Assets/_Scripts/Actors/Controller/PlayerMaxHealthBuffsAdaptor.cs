using BomberGame.Health;
namespace BomberGame
{
    public class PlayerMaxHealthBuffsAdaptor : IMaxHealthBuffable
    {
        public IHealthComponent _health;
        private HealthSettings _defaultSettings;
        private IMaxHealthBuffVFX _maxHealthVFX;

        public PlayerMaxHealthBuffsAdaptor(IHealthComponent health, HealthSettings defaultSettings, IMaxHealthBuffVFX maxHealthVFX)
        {
            _health = health;
            _defaultSettings = defaultSettings;
            _maxHealthVFX = maxHealthVFX;
        }

        public void RestoreMaxHealth()
        {
            _health.SetMaxHealth(_defaultSettings.MaxHealth);
            _maxHealthVFX?.PlayBuff(_defaultSettings.MaxHealth);
        }

        public void SetMaxHealth(int health)
        {
            _health.SetMaxHealth(health);
            _maxHealthVFX?.PlayBuff(_defaultSettings.MaxHealth);

        }




    }
}