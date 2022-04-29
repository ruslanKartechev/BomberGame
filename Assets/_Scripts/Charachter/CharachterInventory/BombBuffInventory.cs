using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombBuffInventory", menuName = "SO/BombBuffInventory", order = 1)]
    public class BombBuffInventory : InventoryBase
    {
        public override void Init()
        {
            
        }
        public override void RemoveItem(string id)
        {
            ItemCount.Remove(id);
        }

        public override string GetCurrentItem()
        {
            return CurrentID;   
        }

        public override void StoreItem(string id, int count)
        {
            ItemCount.Add(id,count);
        }
        public override void ClearInventory()
        {
            ItemCount.Clear();
        }
    }
}