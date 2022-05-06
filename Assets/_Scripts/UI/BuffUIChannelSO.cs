using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using BomberGame;
namespace BomberGame.UI
{
    [CreateAssetMenu(fileName = "BuffUIChannelSO", menuName = "SO/UI/BuffUIChannelSO", order = 1)]
    public class BuffUIChannelSO : ScriptableObject
    {
        public Action<BuffInventory> SetInventory;
        public Action UpdateView;
        public void RaiseUpdateView()
        {
            if (UpdateView != null)
                UpdateView.Invoke();
            else
                Debug.Log("UpdateView action not assigned");
        }
        public void RaiseSetInventory(BuffInventory inventory)
        {
            if (SetInventory != null)
                SetInventory.Invoke(inventory);
            else
                Debug.Log("SetInventory action not assigned");
        }

    }
}