using UnityEngine;
namespace BomberGame
{
    public class HealthManager : IDamagable, IHealable
    {
        public string CharacterID;
        public bool SelfInit = false;
        public event Notifier OnDamage;
        public event Notifier OnDeath;
        protected int _startHealth;
        protected int _health;
        //public int Health { get { return _health; } }
 
        protected bool IsDamagable = false;

        public virtual void Init(int startHealth)
        {
            _health = startHealth;
            _startHealth = startHealth;
        }
        
        public virtual void EnableDamage()
        {
            IsDamagable = true;
        }
        
        public virtual void DisableDamage()
        {
            IsDamagable = false;
        }

        public virtual void Restore()
        {
            _health = _startHealth;
        }

        public virtual void TakeDamage(int damage, string dealer)
        {
            if (!IsDamagable)
                return;
            _health -= damage;
            OnDamage?.Invoke();
            if (_health <= 0)
            {
                DisableDamage();
                OnDeath?.Invoke();
            }
        }

        public virtual void Heal(int addHealth)
        {
            if (_health + addHealth > _startHealth)
                _health = _startHealth;
            else
                _health += addHealth;
        }

    }
}