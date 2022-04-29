using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace CommonGame.Controlls
{
    public abstract class ControllManagerBase : MonoBehaviour
    {
        public abstract void Init(object settings);
        public abstract void EnableControlls();
        public abstract void DisableControlls();
    }

}
