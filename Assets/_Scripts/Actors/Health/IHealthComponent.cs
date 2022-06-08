using System;
namespace BomberGame.Health
{
    public interface IHealthComponent
    {
        void RemoveHealth(int damage);
        void AddHealth(int heal);
        void SetMaxHealth(int health_max);
        int CurrentHealth { get; }
        int MaxHealth { get; }
        float CurrentHealthPercent { get; }

        event EventHander<HealthChangeContext> OnHealthChange;
        event Action OnZeroHealth;

    }
}
namespace BomberGame
{
    public delegate void EventHander<T>(T context);
}