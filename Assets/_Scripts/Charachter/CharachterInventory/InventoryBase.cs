using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    public interface IInventory
    {
        void AddItem(string id,int count);
    }

    public abstract class InventoryBase : ScriptableObject, IInventory
    {
        public Dictionary<string, int> ItemCount = new Dictionary<string, int>();
        public string CurrentID;
        public string DefaultItemID;
        public abstract void Init();
        public abstract string GetCurrentItem();
        public abstract void SetCurrentItem(string id);
        public abstract void AddItem(string id, int count);
        public abstract void RemoveItem(string id);
        public abstract void ClearInventory();

    }
}