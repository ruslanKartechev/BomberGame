using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BuffInventory", menuName = "SO/BuffInventory", order = 1)]
    public class BuffInventory : InventoryBase
    {
        public override void Init()
        {
            
        }
        public override void ClearItem(string id)
        {
            ItemCount.Remove(id);
        }

        public override string GetCurrentItemID()
        {
            return CurrentID;   
        }

        public override bool AddItem(string id, int count)
        {
            string[] parsed = id.Split('_');
            if (parsed.Length <= 1)
            {
                Debug.Log($"Wrong id input: {id}");
                return false;
            }
            string buff = parsed[0];
            int version = Int32.Parse(parsed[1]);
            if (ItemCount.ContainsKey(buff) == false)
            {
                ItemCount.Add(buff, version);
                Debug.Log($"added buff {buff}");
                return true;
            }
            else
            {
                if(ItemCount[buff] != version)
                {
                    ItemCount[buff] = version;
                    Debug.Log($"updated buff {buff} to {version}");
                    return true;
                }
            }
            return false;
        }

        public override void ClearInventory()
        {
            ItemCount.Clear();
        }

        public override void SetCurrentItemID(string id)
        {
           
        }

        public override bool TakeItem(string id, int count)
        {
            if (ItemCount.ContainsKey(id))
            {
                if (ItemCount[id] - count >= 0)
                {
                    ItemCount[id] -= count;
                    return true;
                }
                else
                {
                    Debug.Log($"Cannot return {count} items");
                    return false;
                }

            }
            return false;
        }
    }
}