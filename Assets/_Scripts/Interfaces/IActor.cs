

namespace BomberGame
{
    public interface IActor
    {
        void Init(PlayerSettingsSO _settings, string ID);
        void SetIdle();
        void SetActive();
        void Kill();
    }

}
