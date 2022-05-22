using UnityEngine;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CommonGame.Sound
{

    [System.Serializable]
    public class SimpleMusicPlayer : AudioPlayer
    {
        public VolumeSettings Volume;

        private AudioSourceManager _sourceManager;
        private List<MusicDataSO> _music;

        private AudioSource _currentPlaying;
        public void Init(AudioSourceManager sourceManager, List<MusicDataSO> music, VolumeSettings volume)
        {
            Volume = volume;
            _sourceManager = sourceManager;
            _music = music;
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
            if (_currentPlaying != null)
                _currentPlaying.Stop();
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


        public void PlayRandomMusic()
        {
            if (IsActive == false)
                return;
            MusicInfo sound = GetRandomTrack();
            if(sound.Clip == null)
            {
                return;
            }
            PlayClip(sound);
        }

        private MusicInfo GetRandomTrack()
        {
            if (_music == null || _music.Count == 0)
            {
                Debug.Log("music not assigned");
                return new MusicInfo() ;
            }
            int rand = UnityEngine.Random.Range(0, _music.Count);
            MusicInfo sound = _music[rand].mMusic;
            return sound;
        }

        private async void PlayClip(MusicInfo sound)
        {
            AudioSource source = _sourceManager.GetSource();
            source.volume = sound.Volume * Volume.GetVolume() * _volumeFactor;
            source.pitch = sound.Pitch;
            source.PlayOneShot(sound.Clip);
            _currentPlaying = source;
            await Task.Delay((int)(1000 * sound.Clip.length));
            if (this != null)
            {
                _sourceManager.ReleaseSource(source);
            }
        }


   

    }
}