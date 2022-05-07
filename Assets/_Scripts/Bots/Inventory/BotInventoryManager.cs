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
        private ICharachterBuffer _charachterBuffer;
        public void Init(BombInventory bombInv, BuffInventory buffInv)
        {
            _bombInventory = bombInv;
            _buffInventory = buffInv;
            _bombInventory.DefaultItemID = DefaultBombID;
            _bombInventory.Init();
            _charachterBuffer = GetComponent<ICharachterBuffer>();
            if (_charachterBuffer == null)
            {
                Debug.Log($"Charachter buffer not found {gameObject.name}");
            }
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
        private void OnTriggerEnter2D(Collider2D collider)
        {
            switch (collider.gameObject.tag)
            {
                case Tags.Bomb:
                    IStorable storable = collider.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                    {
                        storable.Store(_bombInventory);
                        string id = storable.GetID();
                        _bombInventory.SetCurrentItemID(id);
                    }
                    break;
                case Tags.BombBuff:
                    storable = collider.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                        storable.Store(_buffInventory);
                    break;
                case Tags.CharachterBuff:
                    BuffProvider provider = collider.gameObject.GetComponent<BuffProvider>();
                    if (provider)
                    {
                        BuffBase buff = provider._myBuff;
                        if (buff)
                        {
                            _charachterBuffer.BuffCharachter(gameObject, buff);
                            provider.Store(null);
                        }
                    }
                    break;
            }
        }
    }
}