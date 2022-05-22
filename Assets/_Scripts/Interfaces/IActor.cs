

namespace BomberGame
{
    public interface IActor
    {
        void Init(PawnSettings _settings, string ID);
        void SetIdle();
        void SetActive();
        void Kill();
    }


    

}
