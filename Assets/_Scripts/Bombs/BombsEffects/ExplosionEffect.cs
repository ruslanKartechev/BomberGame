using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace BomberGame.Bombs
{
    public abstract class ExplosionEffect<T> : MonoBehaviour
    {
        public float Duration;
        public abstract void Play(Vector2 center,  List<ExplosionTarget<T>> results);

    }
}