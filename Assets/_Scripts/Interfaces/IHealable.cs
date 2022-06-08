
namespace BomberGame
{
    public interface IHealable
    {
        void Heal(int health);
        void EnableHealing();
        void DisableHealing();
    }
}