using UnityEngine;

namespace CommonGame.Sound
{
    [System.Serializable]
    public class VolumeSettings
    {
        public float MaxVolume = 1f;
        [Range(0, 1)]
        public float VolumeSlider = 1;

        public float GetVolume()
        {
            return MaxVolume * VolumeSlider;
        }
    }
}