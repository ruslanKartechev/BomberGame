using UnityEngine;

namespace BomberGame
{

    [System.Serializable]
    public struct TimingSettings
    {
        public float DefaultDelay;
        public bool Randomize;
        public float Delay_min;
        public float Delay_max;

        public float GetRandomDelay()
        {
            if (Randomize)
            {
                return UnityEngine.Random.Range(Delay_min, Delay_max);
            }
            else
            {
                return DefaultDelay;
            }
        }
    }



}