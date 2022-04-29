using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class InventorySourceBase : MonoBehaviour
    {
        public abstract InventoryBase GetBombsInventory();
        public abstract InventoryBase GetBombBuffsInventory();
        
    }
}