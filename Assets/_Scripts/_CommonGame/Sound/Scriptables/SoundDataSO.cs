using System.Collections.Generic;
using UnityEngine;


namespace CommonGame.Sound
{
    [CreateAssetMenu(fileName = "SoundDataSO", menuName = "SO/Sound/SoundDataSO", order = 1)]
    public class SoundDataSO : ScriptableObject
    {
        public List<SoundSO> EffectsList = new List<SoundSO>();
        public List<LoopedSoundSO> LoopedEffectsList = new List<LoopedSoundSO>();
        public List<MusicDataSO> MusicList = new List<MusicDataSO>();


    }

}