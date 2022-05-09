using UnityEngine;
using BomberGame.UI;
using System.Collections.Generic;
using System.Linq;
using Zenject;
namespace BomberGame
{
    public class PlayerInventoryManager : CharachterInventory
    {
        [Inject] private BuffMenuBase _buffMenu;
        [Inject] private BombMenuBase _bombMenu;

        private InventoryBase _bombInventory;
        private InventoryBase _buffInventory;
        private ICharachterBuffer _charachterBuffer;

        public void Init(BombInventory bombInv, BuffInventory buffInv)
        {
            _bombInventory = bombInv;
            _buffInventory = buffInv;
            _bombInventory.Init();
            _bombMenu?.SetInventory(bombInv);
            _bombMenu?.UpdateView();
            _buffMenu?.SetInventory(buffInv);
            _buffMenu?.UpdateView();
            _charachterBuffer = GetComponent<ICharachterBuffer>();
            if(_charachterBuffer == null)
            {
                Debug.Log($"Charachter buffer not found {gameObject.name}");
            }
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
            _bombMenu?.UpdateView();
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
            _bombMenu?.UpdateView();
        }

        public void StoreBuff(string id, int count)
        {
            bool added = _buffInventory.AddItem(id, count);
            if (added)
            {
                _buffMenu?.UpdateView();
            }
        }

        public void SetCurrentBomb(string id)
        {
            _bombInventory.SetCurrentItemID(id);
            _bombMenu?.UpdateView();
            _bombMenu?.SetCurrent(id);
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
                        _buffMenu?.UpdateView();
                    }
                    break;
                case Tags.BombBuff:
                    storable = collider.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                        storable.Store(_buffInventory);
                    _buffMenu?.UpdateView();
                    break;
                case Tags.CharachterBuff:
                    BuffProvider provider = collider.gameObject.GetComponent<BuffProvider>();
                    if(provider)
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

        public void ClearInventory()
        {
            _buffInventory?.ClearInventory();
            _bombInventory?.ClearInventory();
            _bombMenu?.UpdateView();
        }

        private void OnDestroy()
        {
            ClearInventory();
        }


    }
}