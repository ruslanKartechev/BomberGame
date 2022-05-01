using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BomberGame.UI;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BombInventory", menuName = "SO/BombInventory", order = 1)]
    public class BombInventory : InventoryBase
    {
        [SerializeField] private BombUIChannelSO _bombMenuChannel;
        private List<string> storedOrder = new List<string>();
        public int StartBombsCount;

        public override void Init()
        {
            AddItem(DefaultItemID, StartBombsCount);
            SetCurrentItem(DefaultItemID);
        }

        public override string GetCurrentItem()
        {
            if (ItemCount.ContainsKey(CurrentID) == false)
            {
                CurrentID = DefaultItemID;
                _bombMenuChannel?.InvokeSetCurrent(CurrentID);
            }
            if (ItemCount[CurrentID] > 0)
            {
                ItemCount[CurrentID]--;
                string retInd = CurrentID;
                if (ItemCount[CurrentID] <= 0)
                {
                    _bombMenuChannel.InvokeRemoveItem(CurrentID);
                    storedOrder.Remove(CurrentID);
                    CurrentID = GetNextIndex();
                    _bombMenuChannel?.InvokeSetCurrent(CurrentID);

                }
                _bombMenuChannel?.InvokeUpdateItem(CurrentID, ItemCount[CurrentID]);
                return retInd;
            }
            else
            {
                _bombMenuChannel.InvokeRemoveItem(CurrentID);
                storedOrder.Remove(CurrentID);

                CurrentID = GetNextIndex();
                _bombMenuChannel?.InvokeSetCurrent(CurrentID);
                _bombMenuChannel?.InvokeUpdateItem(CurrentID, ItemCount[CurrentID]);
            }
            return CurrentID;
        }

        private string GetNextIndex()
        {
            if(storedOrder.Count > 0)
            {
                CurrentID = storedOrder[0];
            }
            else
                CurrentID = DefaultItemID;
            return CurrentID;
        }

        public override void RemoveItem(string id)
        {
            ItemCount.Remove(id);
            _bombMenuChannel?.InvokeRemoveItem(id);
        }

        public override void AddItem(string id, int count)
        {
            Debug.Log($"Storred new bomb {id}, count: {count}");
            if(ItemCount.ContainsKey(id) == false)
            {
                ItemCount.Add(id, count);
                storedOrder.Insert(0,id);
                _bombMenuChannel?.InvokeAddItem(id, count);
            }
            else
            {
                ItemCount[id] += count;
                storedOrder.Insert(0, id);
                _bombMenuChannel?.InvokeUpdateItem(id, ItemCount[id]);
            }
    
        }

        public override void ClearInventory()
        {
            foreach(string id in ItemCount.Keys)
            {
                _bombMenuChannel?.InvokeRemoveItem(id);
            }
            ItemCount.Clear();

        }

        public override void SetCurrentItem(string id)
        {
            CurrentID = id;
            _bombMenuChannel?.InvokeSetCurrent(CurrentID);
        }
    }
}