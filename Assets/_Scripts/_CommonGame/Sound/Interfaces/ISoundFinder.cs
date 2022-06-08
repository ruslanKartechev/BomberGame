using UnityEngine;
namespace CommonGame.Sound
{
    public interface ISoundFinder
    {
        SoundInfo GetSound(string name);

        (SoundInfo, LoopedSoundinfo) GetLoopSound(string name);
    }
}