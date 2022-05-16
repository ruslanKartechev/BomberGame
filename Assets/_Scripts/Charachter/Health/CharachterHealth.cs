using UnityEngine;
using Zenject;
using BomberGame.UI;
namespace BomberGame
{
    public class CharachterHealth : HealthManager
    {
        [SerializeField] private int DebugStartHealth = 2;
        [Space(5)]
        [SerializeField] private DamageEffectBase _effect;

        private void Start()
        {
            if (SelfInit)
            {
                _startHealth = DebugStartHealth;
                _health = _startHealth;
                IsDamagable = true;
            }
        }

        public override void Init(int startHealth)
        {
            _startHealth = startHealth;
            _health = _startHealth;
        }

        public override void EnableDamage()
        {
            base.EnableDamage();
        }

        public override void DisableDamage()
        {
            base.DisableDamage();
        }

        public override void Restore()
        {
            base.Restore();
        }

        public override void TakeDamage(int damage, string dealer)
        {
            base.TakeDamage(damage,dealer);
            _effect?.Execute();

        }
        public override void Heal(int addHealth)
        {
            if (_health + addHealth > _startHealth)
                _health = _startHealth;
            else
                _health += addHealth;
        }
    }
}