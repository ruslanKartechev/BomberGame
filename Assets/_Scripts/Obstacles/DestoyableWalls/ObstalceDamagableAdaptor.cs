using UnityEngine;
using BomberGame.Health;
using System;
namespace BomberGame
{
    public class ObstalceDamagableAdaptor : IDamagable
    {
        public Action OnDeath;
        private IHealthDisplay _healthDisplay;
        private IDestroyEffect _destroyEffect;
        private IHealthComponent _healthComponent;

        public ObstalceDamagableAdaptor(IHealthComponent healthComponent,IHealthDisplay healthDisplay, IDestroyEffect destroyEffect)
        {
            _healthDisplay = healthDisplay;
            _destroyEffect = destroyEffect;
            _healthComponent = healthComponent;
            _healthComponent.OnZeroHealth += OnZeroHealth;
        }

        public void DisableDamage()
        {
        }

        public void EnableDamage()
        {
        }

        public void TakeDamage(int damage, string dealerID)
        {
            _healthComponent.RemoveHealth(1);
            PlayOnDamage();
        }

        private void PlayOnDamage()
        {
            try
            {
                _healthDisplay.DisplayHealth(_healthComponent.CurrentHealthPercent);
            }
            catch
            {
                Debug.Log("Cannot play damage effect");
            }
        }

        private void OnZeroHealth()
        {
            try
            {
                _destroyEffect.PlayDestroyEffect(OnDeath);
            }
            catch
            {
                Debug.Log("Cannot play DestroyEffect");
            }
        }
    }
}