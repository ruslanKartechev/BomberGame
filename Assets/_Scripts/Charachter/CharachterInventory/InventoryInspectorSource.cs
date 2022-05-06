using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class InventoryInspectorSource : InventorySourceBase
    {
        [SerializeField] private BombInventory _bombInventory;
        [SerializeField] private BuffInventory _bombBufferInventory;
        [SerializeField] private bool InitInventory = false;
        [SerializeField] private PlayerInventoryManager _playerInventory;


        private void Start()
        {
            if(_playerInventory == null)
            {
                _playerInventory = GetComponent<PlayerInventoryManager>();
                if(_playerInventory == null)
                {
                    Debug.Log("player inventory not found");
                    return;
                }
            }
            if (InitInventory)
            {
                _playerInventory.Init(_bombInventory, _bombBufferInventory);
            }
        }


        public override InventoryBase GetBombBuffsInventory()
        {
            if (_bombBufferInventory == null)
                Debug.Log("buffer inventory not assinged");
            return _bombBufferInventory;
        }

        public override InventoryBase GetBombsInventory()
        {
            if (_bombInventory == null)
                Debug.Log("bomb inventory not assinged");
            return _bombInventory;
        }
    }
}