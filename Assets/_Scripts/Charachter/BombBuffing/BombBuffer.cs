using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{


    public class BombBuffer : MonoBehaviour, IBombBuffer
    {
        [SerializeField] private InventorySourceBase _inventorySource;
        [SerializeField] private BombBuffers _buffsList;
        private InventoryBase _buffsInventory;
        private void Start()
        {
            _buffsInventory = _inventorySource.GetBombBuffsInventory();
        }

        public void BuffBomb(GameObject target)
        {
            foreach(string id in _buffsInventory.ItemCount.Keys)
            {
                _buffsList.GetBuff(id).Apply(target);
            }
        }
    }
}