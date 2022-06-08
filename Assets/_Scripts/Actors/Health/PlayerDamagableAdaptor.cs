using BomberGame.Health;
using UnityEngine;
using BomberGame.UI;
namespace BomberGame
{
    public class PlayerDamagableAdaptor : IDamagable
    {
        private string _pawnID;
        private IHealthComponent _health;
        public bool AllowSelfDamage = false;
        public bool IsDamagable = true;
        private IDamageVFX _damageVFX;

        public PlayerDamagableAdaptor(string pawnID, IHealthComponent health, IDamageVFX damageVFX)
        {
            _pawnID = pawnID;
            _health = health;
            _damageVFX = damageVFX;
        }

        public void TakeDamage(int damage, string dealerID)
        {
            //Debug.Log("Damage dealer id: " + dealerID + "   MY ID : " + _pawnID);
            if (IsDamagable == false)
                return;
            if (dealerID != _pawnID || AllowSelfDamage)
            {
                _health.RemoveHealth(damage);
                _damageVFX.PlayOnce(1);
            }
        }

        public void EnableDamage()
        {
            IsDamagable = true;
        }

        public void DisableDamage()
        {
            IsDamagable = false;
        }

    }
}