using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public abstract class CharachterMoverBase : MonoBehaviour
    {
        [SerializeField] protected InputMoveChannelSO _inputEvents;

        public abstract void Init(object settings);
        public abstract void EnableMovement();
        public abstract void DisableMovement();
    }
}