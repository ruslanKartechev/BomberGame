using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface IStorable
    {
        public void Store(IInventory inventory);
        public string GetID();
    }
}