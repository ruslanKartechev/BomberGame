using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.UI
{
    public abstract class BuffMenuBase : MonoBehaviour
    {
        public abstract void SetInventory(BuffInventory inventory);
        public abstract void UpdateView();

    }
}