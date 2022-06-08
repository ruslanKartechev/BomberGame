using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombSettingsSO", menuName = "SO/BombSettingsSO", order = 1)]
    public class BombSettingsSO : ScriptableObject
    {
        public float GridSize = 1;
        public int StartExplosionLength = 1;
        public float CountdownTime = 1f;
        [Space(10)]
        public int ExplosionCount = 1;
        public float ExplosionsDelay = 0.1f;
        [Space(10)]
        public int PiercingDepth = 1;
    }

}