
using UnityEngine;
namespace CommonGame.Sound
{
    public class MySoundEffect : MonoBehaviour, ISoundEffect
    {
        public SoundSO _sound;
        public SoundFXChannelSO _soundChannel;
        public bool AllowSoundModify = true;
        public void PlayEffectOnce()
        {
            if(_sound != null)
            {
                if (AllowSoundModify)
                {
                    _soundChannel?.RaisePlay(_sound.mSoundInfo.Name);
                }
                else
                {
                    _soundChannel?.RaisePlayUnmodified(_sound.mSoundInfo.Name);
                }
            }
        }

        public void StartEffect()
        {
            if (_sound != null)
                _soundChannel?.RaiseStartLoop(_sound.mSoundInfo.Name);
        }

        public void StopEffect()
        {
            if (_sound != null)
                _soundChannel?.RaiseStopLoop(_sound.mSoundInfo.Name);
        }
    }
}