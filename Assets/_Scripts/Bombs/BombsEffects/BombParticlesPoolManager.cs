using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame;
namespace BomberGame
{
    public interface IBombParticlesPool
    {
        ParticlesLauncher GetCoundownParticles();

        ParticlesLauncher GetExplosionParticles();

    }
    public class BombParticlesPoolManager : MonoBehaviour, IBombParticlesPool
    {
        [SerializeField] private ParticleLauncherPoolManager _coundownPoolSpawner;
        [SerializeField] private ParticleLauncherPoolManager _explosionPoolSpawner;


        public ParticlesLauncher GetCoundownParticles()
        {
            return _coundownPoolSpawner.GetParticleLauncher();
        }

        public ParticlesLauncher GetExplosionParticles()
        {
            return _explosionPoolSpawner.GetParticleLauncher();

        }
    }
}