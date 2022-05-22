using UnityEngine;
using System;

namespace BomberGame.Health
{

    public class HealthKeeper :IHealth
    {
        private int _health;
        protected int _startHealth;
        protected bool IsDamagable = false;
        protected HealthSettings _settings;

        public int CurrentHealth { get => _health;}

        public event Action OnZeroHealth;
        public virtual void Init(HealthSettings settings)
        {
            _settings = settings;
            _health = _settings.CurrentHealth;
            _startHealth = _settings.CurrentHealth;
            Debug.Log($"Health is set to: {_health}");
        }
        
        public virtual void EnableDamage()
        {
            IsDamagable = true;
        }
        
        public virtual void DisableDamage()
        {
            IsDamagable = false;
        }

        public virtual void Reset()
        {
            _health = _startHealth;
        }

        public virtual void Damage(int damage)
        {
            if (!IsDamagable)
                return;
            _health -= damage;
            if (_health <= 0)
            {
                OnZeroHealth?.Invoke();
            }
        }

        public virtual void Heal(int addHealth)
        {
            _health += addHealth;
            if (_health > _settings.MaxHealth)
                _health = _settings.MaxHealth;
        }

    }
}