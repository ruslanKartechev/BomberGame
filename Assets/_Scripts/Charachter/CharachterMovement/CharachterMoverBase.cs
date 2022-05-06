using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public abstract class CharachterMoverBase : MonoBehaviour
    {
        public InputMoveChannelSO InputMoveChannel;

        public abstract void Init(float moveSpeed);
        public abstract void EnableMovement();
        public abstract void DisableMovement();
    }
}