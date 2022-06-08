using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    public interface IInventory
    {
        bool AddItem(string id,int count);
    }

    public abstract class InventoryBase : ScriptableObject, IInventory
    {
        public Dictionary<string, int> ItemCount = new Dictionary<string, int>();
        public string CurrentID;
        public string DebugAddItemID;
        public int DebugAddItemCount;
        public abstract void Init();
        public abstract string GetCurrentItemID();
        public abstract void SetCurrentItemID(string id);
        public abstract bool TakeItem(string id, int count);
        public abstract bool AddItem(string id, int count);
        public abstract void ClearItem(string id);
        public abstract void ClearInventory();

    }
}