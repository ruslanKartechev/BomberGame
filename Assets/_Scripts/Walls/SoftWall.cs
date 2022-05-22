using System.Collections;
using UnityEngine;
namespace BomberGame
{
    public class SoftWall : Wall, IDamagable
    {
        [SerializeField] private Collider2D _collider;
        public event Notifier OnDestroyed;

        private void Start()
        {
            _map.AddToMap(transform.position, this);
        }

        public override void Damage(DamageApply damage)
        {
            damage.Apply(this);
        }

        void IDamagable.TakeDamage(int damage, string id)
        {
            OnDestroyed?.Invoke();
            _map.RemoveFromMap(this);
            StartCoroutine(Destroying());
        }

        protected virtual IEnumerator Destroying()
        {
            _collider.enabled = false;
            float elapsed = 0f;
            float time = 0.17f;
            while(elapsed <= time)
            {
                transform.localScale = Mathf.Lerp(1.2f, 0f, elapsed / time) * Vector3.one;
                elapsed += Time.deltaTime;
                yield return null;
            }
            Destroy(gameObject);
        }
    }
}