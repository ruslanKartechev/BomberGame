using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.UI {
    public abstract class BombMenuBase : MonoBehaviour
    {
        public abstract void ShowMenu();
        public abstract void HideMenu();
        public abstract void UpdateView();
        public abstract void SetInventory(BombInventory inventory);
        public abstract void SetCurrent(string id);
    }
}