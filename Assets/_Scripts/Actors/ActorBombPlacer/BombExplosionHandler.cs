using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame;
namespace BomberGame
{
    public class BombExplosionHandler : MonoBehaviour
    {
        [SerializeField] private CameraShakeChannelSO _camShakeChannel;

        public void OnExplosion()
        {
            _camShakeChannel?.RaiseEventCameraShake();
        }

    }
}