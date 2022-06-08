using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonGame
{
    [System.Serializable]
    public class ParticlesPainter
    {

        public void SetMaterialColor(List<ParticleSystem> particles, Material material)
        {
            foreach(ParticleSystem p in particles)
            {
                ParticleSystem.MainModule main = p.main;
                main.startColor = material.color;
            }
        }
    }
}