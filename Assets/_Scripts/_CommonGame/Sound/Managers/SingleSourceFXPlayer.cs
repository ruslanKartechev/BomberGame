using UnityEngine;
using System.Collections.Generic;
namespace CommonGame.Sound
{
    [System.Serializable]
    public class SingleSourceFXPlayer : AudioPlayer
    {

        public struct ClipData
        {
            public AudioClip Clip;
            public float clipVolume;
            public AudioSource Source;
        }

        public VolumeSettings Volume;

        private IAudioSourceManager _sourceManager;
        private ISoundFinder _clipFinder;

        private Dictionary<string, ClipData> _setSources = new Dictionary<string, ClipData>();

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

        public void PlaySingleTime(string name)
        {
            if (!IsActive)
                return;
            AudioSource source = null;
            if (_setSources.ContainsKey(name))
            {
                source = _setSources[name].Source;
                source.volume = _setSources[name].clipVolume * Volume.GetVolume() * _volumeFactor;
                source.Stop();
                source.PlayOneShot(_setSources[name].Clip);
            }
            else
            {
                source = _sourceManager.GetSource();
                if (source == null)
                {
                    Debug.Log("AudioSource is null");
                    return;
                }
                SoundInfo sound = _clipFinder.GetSound(name);
                if (sound.Clip == null)
                {
                    Debug.Log($"Sound {name} not found");
                    return;
                }

                ClipData record = new ClipData();
                record.Clip = sound.Clip;
                record.Source = source;
                record.clipVolume = sound.Volume;
                _setSources.Add(name, record);

                source.volume = sound.Volume * Volume.GetVolume() * _volumeFactor;
                source.pitch = sound.Pitch;
                source.PlayOneShot(sound.Clip);
            }
        }

       



    }
}