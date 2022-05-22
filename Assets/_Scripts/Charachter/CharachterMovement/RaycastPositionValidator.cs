using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
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

        public ValidationResult CheckPosition(Vector2 dir, Vector2 from, float distance)
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
                IAdaptableObstacle wall = _raycaster._lastHit.collider.gameObject.GetComponent<IAdaptableObstacle>();
                result.Blocking = wall;
            }
            return result;
        }

        public bool CheckPosition(Vector2 position)
        {
            throw new System.NotImplementedException();
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
                IAdaptableObstacle wall = _raycaster._lastHit.collider.gameObject.GetComponent<IAdaptableObstacle>();
                result.Blocking = wall;
                if (wall != null)
                {
                    switch (wall.GetObstacleType())
                    {
                        case ObstalceType.Movable:
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
        public IAdaptableObstacle Blocking;
    }
}