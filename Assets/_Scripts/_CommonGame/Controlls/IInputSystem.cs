using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CommonGame.Controlls
{
    public interface IInputSystem
    {
        public void Init();
        public void Enable();
        public void Disable();
    }
}