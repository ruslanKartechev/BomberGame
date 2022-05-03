using System.Collections;
using UnityEngine;
namespace BomberGame
{

    public class DestroyableWall : MonoBehaviour, IDamagable
    {
        [SerializeField] private Rigidbody2D _rb;
        [SerializeField] private float _flySpeed;
        [SerializeField] private float _flyTime;
        public event Notifier OnDestroyed;

        public void TakeDamage(int damage)
        {
            OnDestroyed?.Invoke();
            StartCoroutine(Destroying());
        }

        protected virtual IEnumerator Destroying()
        {
            _rb.constraints = RigidbodyConstraints2D.None;
            transform.localScale *= 0.5f;
            _rb.simulated = true;
            _rb.velocity = UnityEngine.Random.insideUnitCircle * _flySpeed;
            yield return new WaitForSeconds(_flyTime);
       
            Destroy(gameObject);
        }
    }
}