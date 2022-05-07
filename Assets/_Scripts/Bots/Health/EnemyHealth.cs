using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class EnemyHealth : HealthManager
    {
        [SerializeField] private DamageEffectBase _effect;

        public override void Init(int startHealth)
        {
            _startHealth = startHealth;
            if(_effect == null)
                _effect = GetComponent<DamageEffectBase>();
        }
        public void UpdateState(BotStates state)
        {
            switch (state)
            {
                case BotStates.Idle:
                    DisableDamage();
                    break;
                case BotStates.Active:
                    EnableDamage();
                    break;
                case BotStates.Dead:
                    DisableDamage();
                    break;
            }
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
            if (dealer == CharacterID)
            {
                return;
            }
            _effect?.Execute();
            base.TakeDamage(damage, dealer);
            
           
        }
        public override void Heal(int health)
        {
            base.Heal(health);

        }
    }
}