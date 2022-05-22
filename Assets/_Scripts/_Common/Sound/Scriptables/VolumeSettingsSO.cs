using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonGame.Sound
{
    [CreateAssetMenu(fileName = "VolumeSettings", menuName = "SO/Sound/VolumeSettingsSO", order = 1)]
    public class VolumeSettingsSO : ScriptableObject
    {
        public VolumeSettings Volume;
    }
}