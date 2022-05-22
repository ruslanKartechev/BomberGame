
using UnityEngine;
namespace CommonGame.Sound
{
    public class MySoundEffect : MonoBehaviour, ISoundEffect
    {
        public SoundSO _sound;
        public SoundFXChannelSO _soundChannel;

        public void PlayEffectOnce()
        {
            _soundChannel?.RaisePlay(_sound.mSoundInfo.Name);
        }

        public void StartEffect()
        {
            _soundChannel?.RaiseStartLoop(_sound.mSoundInfo.Name);
        }

        public void StopEffect()
        {
            _soundChannel?.RaiseStopLoop(_sound.mSoundInfo.Name);
        }
    }
}