
namespace BomberGame
{
    public interface IDamagable
    {
        void TakeDamage(int damage, string dealerID);
        void EnableDamage();
        void DisableDamage();
    }

}