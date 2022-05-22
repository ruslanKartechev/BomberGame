using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class BombCountDown : MonoBehaviour
    {

        public abstract void StartCountdown(float time);
        public abstract void OnExplode();
    }
}