using UnityEngine;
using BomberGame.UI;
using System;
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
                _channel?.RaiseSetHealth(_health.ToString());
            }
        }

        public override void Init(int startHealth)
        {
            _startHealth = startHealth;
            _health = _startHealth;
           _channel?.RaiseSetHealth(_health.ToString());
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

        public override void TakeDamage(int damage)
        {
            base.TakeDamage(damage);
            _effect?.Execute();
            _channel?.RaiseOnDamage();
            _channel?.RaiseSetHealth(_health.ToString());
        }
        public override void Heal(int addHealth)
        {
            if (_health + addHealth > _startHealth)
                _health = _startHealth;
            else
                _health += addHealth;
            _channel?.RaiseSetHealth(_health.ToString());

        }
    }
}