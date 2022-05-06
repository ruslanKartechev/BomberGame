using UnityEngine;
using BomberGame.UI;
using System.Collections.Generic;
using System.Linq;

namespace BomberGame
{
    public class PlayerInventoryManager : CharachterInventory
    {
        public BombUIChannelSO _bombMenuChannel;
        public BuffUIChannelSO _buffMenuChannel;

        private InventoryBase _bombInventory;
        private InventoryBase _buffInventory;

        public void Init(BombInventory bombInv, BuffInventory buffInv)
        {
            _bombInventory = bombInv;
            _buffInventory = buffInv;
            _bombInventory.Init();
            _bombMenuChannel?.RaiseSetInventory(bombInv);
            _bombMenuChannel?.RaiseUpdateView();
            _buffMenuChannel?.RaiseSetInventory(buffInv);
            _buffMenuChannel?.RaiseUpdateView();
        }

        public override string GetBomb()
        {
            string id = _bombInventory.GetCurrentItemID();
            if (_bombInventory.TakeItem(id, 1))
            {

            }
            else
            {
                id = "empty";
            }
            _bombMenuChannel?.RaiseUpdateView();
            return id;

        }
        
        public override Dictionary<string, int> GetBombBuffs()
        {
            if (_buffInventory != null)
                return _buffInventory.ItemCount;
            return null; ;
        }


        public void StoreBomb(string id, int count)
        {
            _bombInventory.AddItem(id, count);
            _bombMenuChannel?.RaiseUpdateView();
        }

        public void StoreBuff(string id, int count)
        {
            bool added = _buffInventory.AddItem(id, count);
            if (added)
            {
                _buffMenuChannel?.RaiseUpdateView();
            }
        }

        public void SetCurrentBomb(string id)
        {
            _bombInventory.SetCurrentItemID(id);
            _bombMenuChannel?.RaiseSetCurrent(id);
            _bombMenuChannel?.RaiseUpdateView();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            switch (collision.gameObject.tag)
            {
                case Tags.Bomb:
                    IStorable storable = collision.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                    {
                        storable.Store(_bombInventory);
                        string id = storable.GetID();
                        _bombInventory.SetCurrentItemID(id);
                        _buffMenuChannel?.RaiseUpdateView();
                    }
                    break;
                case Tags.BombBuff:
                    storable = collision.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                        storable.Store(_buffInventory);
                    _buffMenuChannel?.RaiseUpdateView();
                    break;
            }
        }

        public void ClearInventory()
        {
            _buffInventory?.ClearInventory();
            _bombInventory?.ClearInventory();
            _bombMenuChannel?.RaiseUpdateView();
        }

        private void OnDestroy()
        {
            ClearInventory();
        }


    }
}