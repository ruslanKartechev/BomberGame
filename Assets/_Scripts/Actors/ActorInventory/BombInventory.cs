using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BomberGame.UI;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombInventory", menuName = "SO/BombInventory", order = 1)]
    public class BombInventory : InventoryBase
    {
        private List<string> _itemsInOrder = new List<string>();
        //public List<string> ItemsInOrder { get { return _itemsInOrder; } }
        public override void Init()
        {
            _itemsInOrder.Clear();
            SetCurrentItemID(DebugAddItemID);
        }

        public override string GetCurrentItemID()
        {
            string retInd = CurrentID;
            if (ItemCount.ContainsKey(CurrentID) == false)
            {
                throw new System.Exception($"Inventory does not contain {CurrentID} item");
            }
            if (ItemCount[CurrentID] > 0)
            {
                return CurrentID;
            }

            return DebugAddItemID;
        }

        public override bool TakeItem(string id, int count)
        {
            if (ItemCount.ContainsKey(id))
            {
                if(ItemCount[id] - count >= 0)
                {
                    ItemCount[id] -= count;
                    if (ItemCount[id] <= 0)
                    {
                        _itemsInOrder.Remove(CurrentID);
                        CurrentID = GetNextIndex();
                    }
                    return true;
                }
                else
                {
                    Debug.Log($"Cannot return {count} items with id {id}");
                    return false;
                }

            }
            return false;
        }

        private string GetNextIndex()
        {
            if(_itemsInOrder.Count > 0)
            {
                CurrentID = _itemsInOrder[0];
            }
            else
                CurrentID = DebugAddItemID;
            return CurrentID;
        }

        public override void ClearItem(string id)
        {
            ItemCount.Remove(id);
        }

        public override bool AddItem(string id, int count)
        {
            if(ItemCount.ContainsKey(id) == false)
            {
                ItemCount.Add(id, count);
                _itemsInOrder.Insert(0,id);
            }
            else
            {
                ItemCount[id] += count;
                _itemsInOrder.Insert(0, id);
            }
            return true;
        }

        public override void ClearInventory()
        {
            ItemCount.Clear();
            _itemsInOrder.Clear();
        }

        public override void SetCurrentItemID(string id)
        {
            CurrentID = id;
        }
    }
}