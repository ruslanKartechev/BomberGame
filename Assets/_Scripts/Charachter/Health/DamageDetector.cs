
using UnityEngine;

namespace BomberGame.Health
{
    public class DamageDetector : MonoBehaviour, IDamagable, IHealable
    {
        private string _id;
        private IHealth _health;
        [SerializeField] private Collider2D _collider;
        [SerializeField] private bool _allowSelfDamage = true;
        private void Awake()
        {
            if (_collider == null)
                _collider = GetComponent<Collider2D>();
            _collider.isTrigger = true;
            _collider.enabled = false;
        }
        public void Init(IHealth health, string id)
        {
            _health = health;
            _id = id;
        }

        public void EnableSelfDamage(bool enabled)
        {
            _allowSelfDamage = enabled;
        }

        public void Enable()
        {
            if (_collider)
                _collider.enabled = true;
        }
        public void Disable()
        {
            if(_collider)
                _collider.enabled = false;
        }

        public void TakeDamage(int damage, string dealerID)
        {
            Debug.Log("Took damage");
            if(_allowSelfDamage == true)
            {
                _health?.Damage(damage);
            }
            else
            {
                if (_id != dealerID)
                {
                    _health?.Damage(damage);
                }
            }
         
        }

        public void Heal(int heal)
        {
            _health.Heal(heal);
        }

    }
}