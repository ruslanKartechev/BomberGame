using UnityEngine;
using UnityEngine.UI;
using BomberGame.Health;
namespace BomberGame.UI
{
    public class ActorHealthBlock : MonoBehaviour
    {
        [SerializeField] private Image _barFill;
        private IHealthComponent _healthComponent;
        public void Init(IHealthComponent health)
        {
            _barFill.fillAmount = 1;
            try
            {
                _healthComponent = health;
                _healthComponent.OnHealthChange += OnHealthChange;
            }
            catch
            {
                Debug.LogError("Health component is null. Cannot init UI");
            }
        }

        private void OnHealthChange(HealthChangeContext context)
        {
            float percent = (float)context.CurrentHealth / context.MaxHealth;
            _barFill.fillAmount = percent;
        }
    }

}