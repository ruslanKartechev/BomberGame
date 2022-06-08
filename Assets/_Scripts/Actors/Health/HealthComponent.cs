using UnityEngine;
using System;

namespace BomberGame.Health
{
    public class HealthComponent : IHealthComponent
    {
        protected HealthSettings _settings;

        public int CurrentHealth => _settings.CurrentHealth;

        public int MaxHealth => _settings.MaxHealth;

        public float CurrentHealthPercent { get { return (float)(_settings.CurrentHealth) / _settings.MaxHealth; } }

        public event Action OnZeroHealth;
        public event EventHander<HealthChangeContext> OnHealthChange;

        public virtual void Init(HealthSettings settings)
        {
            _settings = settings;
        }

        public virtual void RemoveHealth(int damage)
        {
            _settings.CurrentHealth -= damage;
            OnHealthChange?.Invoke(new HealthChangeContext(_settings.CurrentHealth, _settings.MaxHealth));
            if (_settings.CurrentHealth <= 0)
            {
                OnZeroHealth?.Invoke();
            }
        }

        public virtual void AddHealth(int addHealth)
        {
            _settings.CurrentHealth += addHealth;
            if (_settings.CurrentHealth > _settings.MaxHealth)
                _settings.CurrentHealth = _settings.MaxHealth;
            OnHealthChange.Invoke(new HealthChangeContext(_settings.CurrentHealth, _settings.MaxHealth));

        }

        public void SetMaxHealth(int health_max)
        {
            _settings.MaxHealth = health_max;
            _settings.CurrentHealth = health_max;
            OnHealthChange.Invoke(new HealthChangeContext(_settings.CurrentHealth, _settings.MaxHealth));

        }
    }
}