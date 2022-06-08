namespace BomberGame.Health
{
    public struct HealthChangeContext
    {
        public int CurrentHealth;
        public int MaxHealth;

        public HealthChangeContext(int currentHealth, int maxHealth)
        {
            CurrentHealth = currentHealth;
            MaxHealth = maxHealth;
        }
    }
}
