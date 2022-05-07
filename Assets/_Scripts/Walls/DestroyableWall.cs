using System.Collections;
using UnityEngine;
namespace BomberGame
{

    public class DestroyableWall : Wall, IDamagable
    {
        [SerializeField] private Collider2D _collider;

        public event Notifier OnDestroyed;
        public void TakeDamage(int damage, string damager)
        {
            OnDestroyed?.Invoke();
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