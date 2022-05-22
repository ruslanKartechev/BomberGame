using UnityEngine;
using System.Threading.Tasks;

namespace CommonGame.Sound
{

    [System.Serializable]
    public class SimpleSoundPlayer : AudioPlayer
    {

        public VolumeSettings Volume;

        private IAudioSourceManager _sourceManager;
        private ISoundFinder _clipFinder;

        public void Init(IAudioSourceManager sourceManager, ISoundFinder clipFinder, VolumeSettings volume)
        {
            Volume = volume;
            _sourceManager = sourceManager;
            _clipFinder = clipFinder;
        }

        #region IAudioPlayer
        public override void Enable()
        {
            _volumeFactor = 1;
            IsActive = true;
        }
        public override void Disable()
        {
            IsActive = false;
        }

        public override void ResumeVolume()
        {
            _volumeFactor = 1;
        }
        public override void Silence()
        {
            _volumeFactor = 0;

        }
        #endregion

        public async void PlaySingleTime(string name)
        {
            AudioSource source = _sourceManager.GetSource();
            if(source == null)
            {
                Debug.Log("AudioSource is null");
                return;
            }
            SoundInfo sound = _clipFinder.GetSound(name);
            if(sound.Clip == null)
            {
                Debug.Log($"Sound {name} not found");
                return;
            }
  
            //source.clip = sound.Clip;
            source.volume = sound.Volume * Volume.GetVolume() * _volumeFactor;
            source.pitch = sound.Pitch;
            source.PlayOneShot(sound.Clip);
            await Task.Delay((int)(1000 * sound.Clip.length));
            if (this != null)
            {
                _sourceManager.ReleaseSource(source);
            }
        }
       

    }
}