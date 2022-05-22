using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BomberGame
{

    public abstract class ExplosionEffect : MonoBehaviour
    {
        public float Duration;
        public abstract void Play(Vector2 center,  List<ExplosionPositions> results);

    }
}