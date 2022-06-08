using UnityEngine;


namespace CommonGame.Sound
{
    [System.Serializable]
    public struct MusicInfo
    {
        public string Name;
        [Range(0f, 1f)]
        public float Volume;
        [Range(0f, 1f)]
        public float Pitch;
        public AudioClip Clip;
    }

}