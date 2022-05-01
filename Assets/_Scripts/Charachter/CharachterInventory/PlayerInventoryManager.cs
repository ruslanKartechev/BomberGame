using UnityEngine;
namespace BomberGame
{
    public class PlayerInventoryManager : MonoBehaviour
    {
        [SerializeField] private InventorySourceBase _inventorySource;

        private InventoryBase _bombInventory;
        private InventoryBase _buffInventory;


        private void Start()
        {
            _bombInventory = _inventorySource.GetBombsInventory();
            _buffInventory = _inventorySource.GetBombBuffsInventory();
        }

        public void GetBomb(string id, int count)
        {
            _bombInventory.AddItem(id, count);
        }

        public void GetBuff(string id, int count)
        {
            _buffInventory.AddItem(id, count);
        }

        public void SetCurrentBomb(string id)
        {
            _bombInventory.SetCurrentItem(id);

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
                        _bombInventory.SetCurrentItem(id);
                    }
                    break;
                case Tags.BombBuff:
                    storable = collision.gameObject.GetComponent<IStorable>();
                    if (storable != null)
                        storable.Store(_buffInventory);
                    break;
            }
        }

        public void ClearInventory()
        {
            _buffInventory?.ClearInventory();
            _bombInventory?.ClearInventory();
        }
        
        private void OnDestroy()
        {
            ClearInventory();
        }


    }
}