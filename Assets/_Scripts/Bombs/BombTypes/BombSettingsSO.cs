using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombSettingsSO", menuName = "SO/BombSettingsSO", order = 1)]
    public class BombSettingsSO : ScriptableObject
    {
        public float GridSize = 1;
        public int StartExplosionLength = 10;
        public float CountdownTime = 1f;
        [Space(10)]
        public int Damage = 1;
        [Space(10)]
        public int ExplosionCount = 1;
        public float ExplosionsDelay = 0.1f;
        [Space(10)]
        public LayerMask CastMask;
        public float CircleCastRad;
        public List<Vector3> CastDirections = new List<Vector3>();
        [Space(10)]
        public int PiercingDepth = 1;
        public bool UsePiercingDepth = true;
    }

}