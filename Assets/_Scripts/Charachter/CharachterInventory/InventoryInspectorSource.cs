using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class InventoryInspectorSource : InventorySourceBase
    {
        [SerializeField] private InventoryBase _bombInventory;
        [SerializeField] private InventoryBase _bombBufferInventory;
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