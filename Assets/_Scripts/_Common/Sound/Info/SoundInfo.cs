using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace CommonGame.Sound
{
    [System.Serializable]
    public struct SoundInfo
    {
        public string Name;
        [Range(0f,1f)]
        public float Volume;
        [Range(0f, 1f)]
        public float Pitch;
        public AudioClip Clip;
    }
}