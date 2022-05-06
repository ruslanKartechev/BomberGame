using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [System.Serializable]
    public class DirectionGenerator
    {

        public Vector3 GetDirection()
        {
            Vector3 dir = Vector3.right;
            int random = UnityEngine.Random.Range(0,4);
            switch (random)
            {
                case 0:
                    dir = Vector3.up;
                    break;
                case 1:
                    dir = Vector3.right;
                    break;
                case 2:
                    dir = -Vector3.up;
                    break;
                case 3:
                    dir = -Vector3.right;
                    break;
            }
            return dir;
        }

        public Vector3 GetDirectionOther(Vector3 otherThan)
        {
            Vector3 result = Vector3.right;
            do
            {
                result = GetDirection();
            } while (result == otherThan);
            return result;
        }
    }
}