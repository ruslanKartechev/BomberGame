using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class TestBomb : MonoBehaviour, IStorable
    {
        [SerializeField] private string ID;
        [SerializeField] private int StoreAmount;
        public string GetID()
        {
            return ID;
        }

        public void Store(IInventory inventory)
        {
            inventory.StoreItem(ID, StoreAmount);
            Destroy(gameObject);
        }

    }
}