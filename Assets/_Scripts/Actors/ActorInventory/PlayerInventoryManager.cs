using UnityEngine;
using BomberGame.UI;
using System.Collections.Generic;
using System.Linq;
using Zenject;
namespace BomberGame
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        [Inject] private BombMenuBase _bombMenu;

        private InventoryBase _bombInventory;

        public void Init(BombInventory bombInv)
        {
            _bombInventory = bombInv;
            _bombInventory.Init();
            _bombMenu?.SetInventory(bombInv);
            _bombMenu?.UpdateView();
        }

        public string GetBomb()
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

        public void StoreBomb(string id, int count)
        {
            _bombInventory.AddItem(id, count);
            _bombMenu?.UpdateView();
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
                    }
                    break;
                case Tags.BombBuff:

                    break;
                case Tags.CharachterBuff:

                    break;
            }
        }

        public void ClearInventory()
        {
            _bombInventory?.ClearInventory();
            _bombMenu?.UpdateView();
        }

        private void OnDestroy()
        {
            ClearInventory();
        }


    }
}