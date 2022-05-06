using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace BomberGame.UI
{
    [CreateAssetMenu(fileName = "BombUIChannelSO", menuName = "SO/UI/BombUIChannelSO", order = 1)]
    public class BombUIChannelSO : ScriptableObject
    {
        public Action ShowMenu;
        public Action HideMenu;
        public Action UpdateView;
        public Action<BombInventory> SetInventory;
        public Action<string> SetCurrent;

        public void RaiseShow()
        {
            if(ShowMenu != null)
            {
                ShowMenu?.Invoke();
            }
            else
            {
                Debug.Log("ShowMenu aciton not assined");
                return;
            }
        }

        public void RaiseHide()
        {
            if (HideMenu != null)
            {
                HideMenu?.Invoke();
            }
            else
            {
                Debug.Log("HideMenu aciton not assined");
                return;
            }
        }

        public void RaiseUpdateView()
        {
            if (UpdateView != null)
            {
                UpdateView?.Invoke();
            }
            else
            {
                Debug.Log("UpdateView aciton not assined");
                return;
            }
        }
        
        public void RaiseSetInventory(BombInventory inventory)
        {
            if (SetInventory != null)
                SetInventory.Invoke(inventory);
            else
                Debug.Log("SetInventory action not assigned");
        }

        public void RaiseSetCurrent(string id)
        {
            if (SetCurrent != null)
                SetCurrent.Invoke(id);
            else
                Debug.Log("SetCurrent not set");
        }

    }
}