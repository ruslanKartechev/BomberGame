using UnityEngine;

namespace CommonGame.Sound
{
    public interface ISoundLoopManager
    {
        void PlayOnLoop(string name);
        void StopLoop(string SoundName);
    }

}