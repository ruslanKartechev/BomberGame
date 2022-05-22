namespace BomberGame.Health
{
    public interface IHealth
    {
        void Damage(int damage);
        void Heal(int heal);
    }
}