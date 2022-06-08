using BomberGame.Bombs;
namespace BomberGame
{
    public interface IBombBuffer
    {
        void AddBuff(BombBuffBase buff);
        void RemoveBuff(BombBuffBase buff);
        void ApplyBuffs(Bomb target);

    }
}