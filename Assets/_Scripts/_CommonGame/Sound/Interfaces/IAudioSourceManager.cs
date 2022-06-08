using UnityEngine;
namespace CommonGame.Sound
{
    public interface IAudioSourceManager
    {
        void Init();
        AudioSource GetSource();
        void ReleaseSource(AudioSource source);
    }
}