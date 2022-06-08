namespace BomberGame
{
    public interface ISpeedBuffable
    {
        void BuffSpeed(float speedModifier);
        void RestoreOriginal();
    }

}
