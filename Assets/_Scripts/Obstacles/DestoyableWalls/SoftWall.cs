using System.Collections;
using UnityEngine;
using BomberGame.Health;
namespace BomberGame
{
    public class SoftWall : Obstacle
    {
        [SerializeField] private HealthSettings _healthSettings;
        [SerializeField] private ObstalceDamagableAdaptor _damagableAdaptor;
        private HealthComponent _healthComponent;
        private IHealthDisplay _healthDisplay;
        private IDestroyEffect _destroyEffect;
        private void Awake()
        {
            _healthDisplay = GetComponent<IHealthDisplay>();
            _destroyEffect = GetComponent<IDestroyEffect>();

        }
        private void Start()
        {
            _map.AddToMap(transform.position, this);
            InitHealth();

        }
        private  void InitHealth()
        {
            _healthComponent = new HealthComponent();
            _healthComponent.Init(_healthSettings);
            _damagableAdaptor = new ObstalceDamagableAdaptor(_healthComponent, _healthDisplay, _destroyEffect);
            _damagableAdaptor.OnDeath = OnDestroyed;
            _obstacleComponents.Add(typeof(IDamagable), _damagableAdaptor);
        }

        public void OnDestroyed()
        {
            _map.RemoveFromMap(this);
            gameObject.SetActive(false);
        }

    }
}