using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface IPositionValidator
    {
        ValidationResult CheckPosition(Vector3 dir, Vector3 from, float distance);
        
    }


    public class RaycastPositionValidator : IPositionValidator
    {
       private CircleCaster _raycaster;
        public RaycastPositionValidator(CircleCaster caster)
        {
            _raycaster = caster;
            if(_raycaster == null)
            {
                Debug.Log("Passed null raycaster");
            }
        }

        public ValidationResult CheckPosition(Vector3 dir, Vector3 from, float distance)
        {
            ValidationResult result = new ValidationResult();
            result.Allow = false;
            _raycaster.Distance = distance;
            _raycaster.Raycast(from, dir);
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

        public ValidationResult CheckPositionPush(Vector3 dir, Vector3 from, float distance, float moveTime)
        {
            ValidationResult result = new ValidationResult();
            result.Allow = false;
            _raycaster.Distance = distance;
            _raycaster.Raycast(from, dir);
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

    public struct ValidationResult
    {
        public bool Allow;
        public IWall BlockingWall;
    }
}