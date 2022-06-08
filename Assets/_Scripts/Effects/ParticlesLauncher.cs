using UnityEngine;
using System.Collections.Generic;
namespace CommonGame
{
    public class ParticlesLauncher : EffectLauncherBase
    {
        [SerializeField] protected List<ParticleSystem> _particles = new List<ParticleSystem>();


        public override void Init()
        {
            foreach(ParticleSystem p in _particles)
            {
                if (p != null)
                {
                    ParticleSystem.MainModule main = p.main;
                    main.playOnAwake = false;
                    p?.Stop();
                }
            }
        }

        public override void Play()
        {
            if (_particles == null)
                return;
            foreach (ParticleSystem p in _particles)
            {
                p?.Play();
            }
        }

        public override void Stop()
        {
            if (_particles == null)
                return;
            foreach (ParticleSystem p in _particles)
            {
                p?.Stop();
            }
        }

        public List<ParticleSystem> GetParticles()
        {
            return _particles;
        }
    }

}