
using UnityEngine;
using Zenject;
using CommonGame;
using System.Collections;

namespace CommonGame.Sound
{

    public class SoundSystem : MonoBehaviour, ISoundSystem, IMusicPlayer, ICoroutineRunner
    {
        [Inject] private SoundFXChannelSO _soundChannel;
        [Inject] private SoundDataSO _soundData;

        [Space(10)]
        public VolumeSettingsSO SoundFXVolume;
        public VolumeSettingsSO MusicVolume;

        [Space(10)]
        [SerializeField] private SingleSourceFXPlayer _sfxManager;
        [SerializeField] private SoundLoopManager _loopManager;
        [SerializeField] private SimpleMusicPlayer _musicManager;
        [Space(10)]
        [SerializeField] private AudioSourceManager _sourcesManager;
        [SerializeField] private SOListSoundFinder _clipFinder;
        

        public void Init()
        {
            _soundChannel.PlayFX = PlaySingleTime;
            _soundChannel.PlayLoopedFX = StartSoundEffect;
            _soundChannel.StopPlayingLoop = StopLoopedEffect;

            _sourcesManager.Init();
            _clipFinder.Init(_soundData.EffectsList, _soundData.LoopedEffectsList);

            _sfxManager?.Init(_sourcesManager, _clipFinder, SoundFXVolume.Volume);
            _sfxManager?.Enable();
            _loopManager?.Init(_clipFinder, _sourcesManager, this,SoundFXVolume.Volume);
            _loopManager?.Enable();
            _musicManager.Init(_sourcesManager, _soundData.MusicList, MusicVolume.Volume);
            _musicManager?.Enable();
        }
        
        public void PlaySingleTime(string name)
        {
            _sfxManager?.PlaySingleTime(name);
        }

        public void StartSoundEffect(string name)
        {
            _loopManager?.PlayOnLoop(name);
        }

        public void StopLoopedEffect(string name)
        {
            _loopManager?.StopLoop(name);
        }

        public void PlayMusic()
        {
            _musicManager.PlayRandomMusic();
        }
        
        public void DisableSound()
        {

        }


        #region ICoroutineRunner
        public Coroutine StartRoutine(IEnumerator enumerator)
        {
            return StartCoroutine(enumerator);
        }

        public void StopRoutine(Coroutine routine)
        {
            if (routine != null)
                StopCoroutine(routine);
        }
        #endregion
    }
}