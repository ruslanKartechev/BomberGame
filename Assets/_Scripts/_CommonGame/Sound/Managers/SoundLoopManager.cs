using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CommonGame.Sound
{




    [System.Serializable]
    public class SoundLoopManager : AudioPlayer, ISoundLoopManager
    {
        public VolumeSettings Volume;

        private AudioSourceManager _sourceManager;
        private List<Loop> _activeLoops = new List<Loop>();
        private ISoundFinder _soundFinder;
        private ICoroutineRunner _routineRunner;

        public void Init(ISoundFinder soundFinder, AudioSourceManager sourceManager, ICoroutineRunner routineRunner, VolumeSettings volume)
        {
            Volume = volume;
            _soundFinder = soundFinder;
            _sourceManager = sourceManager;
            _routineRunner = routineRunner;
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
            foreach (Loop l in _activeLoops)
            {
                StopLoop(l.Name);
            }
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

        public void PlayOnLoop(string name)
        {
            if (!IsActive)
                return;
            (SoundInfo, LoopedSoundinfo) info = _soundFinder.GetLoopSound(name);
            if (info.Item1.Clip == null)
            {
                Debug.Log($"Clip {name} was not found");
                return;
            }
            AudioSource source = _sourceManager.GetSource();
            StartLoop(source, info.Item1, info.Item2, Volume.MaxVolume * Volume.VolumeSlider);
        }

        private void StartLoop(AudioSource source, SoundInfo sound, LoopedSoundinfo loopinfo, float volumeMultiplier)
        {
            Loop loop = _activeLoops.Find(x => x.Name == sound.Name);
            if (loop.LoopRoutine != null)
                _routineRunner.StopRoutine(loop.LoopRoutine);
            loop.Source?.Stop();
            _activeLoops.Remove(loop);
            Coroutine looping = _routineRunner.StartRoutine(PlayOnLoop(source, sound, loopinfo, volumeMultiplier));
            _activeLoops.Add(new Loop(sound.Name, looping, source));
        }

        public void StopLoop(string SoundName)
        {
            Loop myLoop = _activeLoops.Find(x => x.Name == SoundName);
            if (myLoop.LoopRoutine != null)
                _routineRunner.StopRoutine(myLoop.LoopRoutine);
            myLoop.Source?.Stop();
            _activeLoops.Remove(myLoop);
            if (this != null)
            {
                _sourceManager.ReleaseSource(myLoop.Source);
            }
        }

        private IEnumerator PlayOnLoop(AudioSource source, SoundInfo sound, LoopedSoundinfo loopInfo, float volume)
        {
            source.clip = sound.Clip;
            source.pitch = sound.Pitch;
            source.volume = sound.Volume * Volume.GetVolume() * _volumeFactor;
            source.loop = false;
            source.Play();

            if(loopInfo.LoopedTime_start == 0  && loopInfo.LoopedTime_end == 0)
            {
                while (true)
                {
                    yield return new WaitForSeconds(sound.Clip.length);
                    source.time = 0;
                }
            }
            else
            {
                yield return new WaitForSeconds(loopInfo.LoopedTime_start);
                while (true)
                {
                    source.time = loopInfo.LoopedTime_start;
                    source.Play();
                    yield return new WaitForSeconds(loopInfo.LoopedTime_end - loopInfo.LoopedTime_start);

                }
            }
        }

    }

}