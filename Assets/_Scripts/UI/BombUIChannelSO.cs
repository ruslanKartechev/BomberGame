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

        public Action<string, int> AddItem;
        public Action<string, int> UpdateCount;

        public Action<string> RemoveItem;
        public Action<string> SetCurrentItem;

        public void InvokeShow()
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
        public void InvokeHide()
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
        public void InvokeAddItem(string id, int count)
        {
            if (AddItem != null)
            {
                AddItem?.Invoke(id,count);
            }
            else
            {
                Debug.Log("AddItem aciton not assined");
                return;
            }
        }

        public void InvokeUpdateItem(string id, int count)
        {
            if (UpdateCount != null)
            {
                UpdateCount?.Invoke(id, count);
            }
            else
            {
                Debug.Log("AddItem aciton not assined");
                return;
            }
        }
        public void InvokeRemoveItem(string id)
        {
            if (RemoveItem != null)
            {
                RemoveItem?.Invoke(id);
            }
            else
            {
                Debug.Log("AddItem aciton not assined");
                return;
            }
        }
        public void InvokeSetCurrent(string id)
        {
            if(SetCurrentItem != null)
            {
                SetCurrentItem?.Invoke(id);
            }
            else
            {
                Debug.Log($"SetCurrentItem action not assigned");
            }
        }

    }
}