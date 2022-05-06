using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace BomberGame
{
    public class BotInventoryManager : CharachterInventory
    {
        public string DefaultBombID;
        private InventoryBase _bombInventory;
        private InventoryBase _buffInventory;
        public void Init(BombInventory bombInv, BuffInventory buffInv)
        {
            _bombInventory = bombInv;
            _buffInventory = buffInv;
            _bombInventory.DefaultItemID = DefaultBombID;
            _bombInventory.Init();
        }
        public override string GetBomb()
        {
            if (_bombInventory == null)
                return "empty";
            string id = _bombInventory.GetCurrentItemID();
            if (_bombInventory.TakeItem(id, 1))
            {
                return id;
            }
            else
            {
                return DefaultBombID;
            }
        }

        public override Dictionary<string, int> GetBombBuffs()
        {
            if (_buffInventory != null)
                return _buffInventory.ItemCount;
            return null; ;
        }
    }
}