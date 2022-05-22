
using UnityEngine;
using Zenject;
namespace BomberGame
{
    [DefaultExecutionOrder(0)]
    public class Wall : MonoBehaviour, IAdaptableObstacle
    {
        [SerializeField] protected ObstalceType _type;
        [Inject] protected IObstacleMap _map;

        private void Start()
        {
            _map.AddToMap(transform.position, this);
        }

        public virtual void Damage(DamageApply damage)
        {

        }

        public ObstalceType GetObstacleType()
        {
            return _type;
        }
    }
}