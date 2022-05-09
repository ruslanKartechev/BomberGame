

namespace BomberGame
{
    public interface IPlayer
    {
        void Init(PlayerSettingsSO _settings, string ID);
        void SetIdle();
        void SetActive();
        void Kill();
    }

}
