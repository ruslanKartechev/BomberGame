using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombInventory", menuName = "SO/BombInventory", order = 1)]
    public class BombInventory : InventoryBase
    {
        public int StartBombsCount;

        public override void Init()
        {
            CurrentID = DefaultItemID;
            ItemCount.Add(DefaultItemID,1);
        }
        public override string GetCurrentItem()
        {
            if (ItemCount.ContainsKey(CurrentID) == false || ItemCount[CurrentID] <= 0)
            {
                CurrentID = DefaultItemID;
            }
            ItemCount[CurrentID]--;
            return CurrentID;
        }

        public override void RemoveItem(string id)
        {
            ItemCount.Remove(id);
        }

        public override void StoreItem(string id, int count)
        {
            Debug.Log($"storred new item {id}, count: {count}");
            if(ItemCount.ContainsKey(id) == false)
            {
                ItemCount.Add(id, count);
            }
            else
            {
                ItemCount[id] += count;
            }
        }
        public override void ClearInventory()
        {
            ItemCount.Clear();
        }

    }
}