using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class LootDropper : MonoBehaviour
    {
        [SerializeField] protected SoftWall _wall;
        [SerializeField] protected GameObject _droppable;
        private void Awake()
        {
            if (_droppable == null)
                return;
            if (_wall == null)
                _wall = GetComponent<SoftWall>();
            if(_wall != null)
            {
                _wall.OnDestroyed += DropBuff;
            }
        }

        protected void DropBuff()
        {
            if (_droppable == null)
                return;
            IDroppable droppable = _droppable.GetComponent<IDroppable>();
            droppable?.Drop();
        }
    }
}