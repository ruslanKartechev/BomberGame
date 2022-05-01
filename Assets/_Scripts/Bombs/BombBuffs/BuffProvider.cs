using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame {


    public class BuffProvider : MonoBehaviour, IStorable
    {
        [SerializeField] protected BombBuffBase _myBuff;
        [SerializeField] protected Collider2D _collider;

        public string GetID()
        {
            return _myBuff.ID;
        }

        public virtual void Store(IInventory inventory)
        {
            Debug.Log($"stored new buff {_myBuff.ID}");
            inventory.AddItem(_myBuff.ID,1);
            if(_collider != null)
                _collider.enabled = false;
            
            
            Destroy(gameObject);
        }



    }
}