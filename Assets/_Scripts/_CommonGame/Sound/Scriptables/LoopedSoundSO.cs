using UnityEngine;

namespace CommonGame.Sound
{
    [CreateAssetMenu(fileName = "LoopedSoundSO", menuName = "SO/Sound/LoopedSoundSO", order = 1)]
    public class LoopedSoundSO : ScriptableObject
    {
        public SoundInfo mSoundInfo;
        public LoopedSoundinfo mLoopInfo;
    }
}