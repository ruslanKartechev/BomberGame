using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
   [System.Serializable]
    public class CharachterBombPositioner
    {
        [SerializeField] private CharachterTileMover _tileMover;

        public Vector3 GetPosition()
        {
            if (_tileMover != null)
                return _tileMover.PrevTilePosition;
            else
                return Vector3.zero;
        }

    }
}