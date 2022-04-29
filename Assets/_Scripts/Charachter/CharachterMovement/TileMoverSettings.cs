using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "TileMoverSettings", menuName = "SO/TileMoverSettings", order = 1)]
    public class TileMoverSettings : ScriptableObject
    {
        public float GridDistance = 0.1f;
        public float SnapTime = 0.2f;
        public float Speed = 1f;
        public float GetTime()
        {
            return SnapTime / Speed;
        }

    }
}