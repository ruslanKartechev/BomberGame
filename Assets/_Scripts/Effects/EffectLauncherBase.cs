using UnityEngine;

namespace CommonGame
{
    public abstract class EffectLauncherBase : MonoBehaviour
    {
        public abstract void Init();
        public abstract void Play();
        public abstract void Stop();
    }
}