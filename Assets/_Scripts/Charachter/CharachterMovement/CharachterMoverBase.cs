using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Controlls;
using Zenject;
namespace BomberGame
{
    public abstract class CharachterMoverBase 
    {
        public abstract void Init();
        public abstract void Move(Vector3 direction);
    }
}