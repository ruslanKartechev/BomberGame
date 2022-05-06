using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{

    [System.Serializable]
    public class PositionChecker
    {
        [SerializeField]  private CircleCaster _raycaster;
        public Transform _charachter;

        public PositionChecker(CircleCaster caster, Transform charachter, LayerMask mask)
        {
            _raycaster = caster;
            _charachter = charachter;
            _raycaster.Mask = mask;
        }
        public void SetMask(LayerMask mask)
        {
            _raycaster.Mask = mask;
        }

        public CasterResult CheckPosition(Vector3 dir, float distance)
        {
            CasterResult result = new CasterResult();
            result.Allow = false;
            _raycaster.Distance = distance;
            _raycaster.Raycast(_charachter.position, dir);
            if (_raycaster._lastHit == false)
            {
                result.Allow = true;
            }
            else
            {
                IWall wall = _raycaster._lastHit.collider.gameObject.GetComponent<IWall>();
                result.BlockingWall = wall;
            }
            return result;
        }

        public CasterResult CheckPositionPush(Vector3 dir, float distance, float moveTime)
        {
            CasterResult result = new CasterResult();
            result.Allow = false;
            _raycaster.Distance = distance;
            _raycaster.Raycast(_charachter.position, dir);
            if (_raycaster._lastHit == false)
            {
                result.Allow = true;
            }
            else
            {
                IWall wall = _raycaster._lastHit.collider.gameObject.GetComponent<IWall>();
                result.BlockingWall = wall;
                if (wall != null)
                {
                    switch (wall.GetType())
                    {
                        case WallType.Movable:
                            IMovableWall m = _raycaster._lastHit.collider.gameObject.GetComponent<IMovableWall>();
                            bool canMove = m.Move(dir, distance, moveTime);
                            if (canMove == true)
                            {
                                result.Allow = true;
                            }
                            break;
                    }
                }
            }
            return result;
        }

    }

    public struct CasterResult
    {
        public bool Allow;
        public IWall BlockingWall;
    }
}