using UnityEngine;
using System.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
namespace CommonGame.Sound
{

    [System.Serializable]
    public class SimpleSoundPlayer : AudioPlayer
    {
        public VolumeSettings Volume;

        private IAudioSourceManager _sourceManager;
        private ISoundFinder _clipFinder;
        private ISoundInfoModifier _soundModifier;


        public void Init(IAudioSourceManager sourceManager, ISoundFinder clipFinder, VolumeSettings volume)
        {
            Volume = volume;
            _sourceManager = sourceManager;
            _clipFinder = clipFinder;
        }

        public void InitSoundModifier(ISoundInfoModifier modifier)
        {
            _soundModifier = modifier;
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

        public async void PlaySingleTime(string name, bool modified = true)
        {
            AudioSource source = _sourceManager.GetSource();
            if(source == null)
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
            if(modified)
                sound = GetModifiedSound(sound);
            PlayClip(source, sound);
            await Task.Delay((int)(1000 * sound.Clip.length));
            if (this != null)
                _sourceManager.ReleaseSource(source);
        }

        private void PlayClip(AudioSource source, SoundInfo sound)
        {
            //source.clip = sound.Clip;
            source.volume = sound.Volume * Volume.GetVolume() * _volumeFactor;
            source.pitch = sound.Pitch;
            source.PlayOneShot(sound.Clip);
        }

        private SoundInfo GetModifiedSound(SoundInfo sound)
        {
            if (_soundModifier != null)
            {
                sound = _soundModifier.Apply(sound);
            }
            return sound;
        }
       

    }



    [System.Serializable]
    public struct PitchRaiseSettings
    {
        public float MaxTimeDelay;
        public float Pitch_min;
        public float Pitch_max;
        public int CallToMax;
    }


    public interface ISoundInfoModifier
    {
        SoundInfo Apply(SoundInfo input);
    }


    [System.Serializable]
    public class PitchRaiserPlayer : ISoundInfoModifier
    {
        [SerializeField] private PitchRaiseSettings _settings;
        public bool NameMatters = false;
        private Dictionary<string, float> _timeTable = new Dictionary<string, float>();
        private Dictionary<string, int> _callsTable = new Dictionary<string, int>();

        private float _lastCallTime = 0;
        private int _count = 0;
        public SoundInfo Apply(SoundInfo input)
        {
            if (NameMatters)
            {
                input.Pitch = GetPitchNameMatters(input.Name);
            }
            else
            {
                input.Pitch = GetPitchNoName();
            }
            return input;
        }


        private float GetPitchNoName()
        {
            float time = GetLastCallTimeNoName();
            int callsCount = GetCallsCountNoName(time); 
            float pitch = GetPitch(callsCount);
            return pitch;
        }
        private float GetPitchNameMatters(string name)
        {
            float time = GetLastCallTime(name);
            int callsCount = GetCallsCount(name, time);
            float pitch = GetPitch(callsCount);
            return pitch;
        }

        private float GetLastCallTimeNoName()
        {
            float val = _lastCallTime;
            _lastCallTime = Time.time;
            return val;
        }


        private int GetCallsCountNoName(float lastPlayTime)
        {
            float timeDelay = Time.time - lastPlayTime;
            if (timeDelay < _settings.MaxTimeDelay)
            {
                _count++;
            }
            else
            {
                _count = 0;
            }
            return _count;
        }

        private float GetLastCallTime(string name)
        {
            float currentTime = Time.time;
            if (_timeTable.ContainsKey(name))
            {
                float lastTime = _timeTable[name];
                _timeTable[name] = currentTime;
                return lastTime;
            }
            else
            {
                _timeTable.Add(name, currentTime);
                return currentTime;
            }
        }



        private int GetCallsCount(string name, float lastPlayTime)
        {
            float timeDelay = Time.time - lastPlayTime ;
            if (_callsTable.ContainsKey(name))
            {
                if(timeDelay < _settings.MaxTimeDelay)
                {
                    int count = _callsTable[name]++;
                    return count;
                }
                else
                {
                    _callsTable[name] = 0;
                    return 0;
                }
            }
            else
            {
                _callsTable.Add(name, 0);
                return 0;
            }
            
        }


        public float GetPitch(int callsCount)
        {
            float pitch = Mathf.Lerp(_settings.Pitch_min, _settings.Pitch_max, (float)callsCount / _settings.CallToMax);
            return pitch;
        }


    }










}