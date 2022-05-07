using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame {


    public class BuffProvider : MonoBehaviour, IStorable
    {
        public BuffBase _myBuff;
        [SerializeField] protected Collider2D _collider;

        public string GetID()
        {
            return _myBuff.ID;
        }

        public virtual void Store(IInventory inventory)
        {
            if (inventory != null)
            {
                inventory.AddItem(_myBuff.ID, 1);
                if (_collider != null)
                    _collider.enabled = false;
            }
            Destroy(gameObject);
        }



    }
}