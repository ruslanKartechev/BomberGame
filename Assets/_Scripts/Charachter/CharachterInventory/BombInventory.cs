using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BomberGame.UI;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombInventory", menuName = "SO/BombInventory", order = 1)]
    public class BombInventory : InventoryBase
    {
        private List<string> _storedOrder = new List<string>();
        public List<string> ItemsInOrder { get { return _storedOrder; } }
        public int StartBombsCount;

        public override void Init()
        {
            _storedOrder.Clear();
            AddItem(DefaultItemID, StartBombsCount);
            SetCurrentItemID(DefaultItemID);
        }

        public override string GetCurrentItemID()
        {
            string retInd = CurrentID;
            if (ItemCount.ContainsKey(CurrentID) == false)
            {
                CurrentID = DefaultItemID;
            }
            if (ItemCount[CurrentID] > 0)
            {
                return CurrentID;
            }

            return DefaultItemID;
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
                        _storedOrder.Remove(CurrentID);
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
            if(_storedOrder.Count > 0)
            {
                CurrentID = _storedOrder[0];
            }
            else
                CurrentID = DefaultItemID;
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
                _storedOrder.Insert(0,id);
            }
            else
            {
                ItemCount[id] += count;
                _storedOrder.Insert(0, id);
            }
            return true;
        }

        public override void ClearInventory()
        {
            ItemCount.Clear();
            _storedOrder.Clear();
        }

        public override void SetCurrentItemID(string id)
        {
            CurrentID = id;
        }
    }
}