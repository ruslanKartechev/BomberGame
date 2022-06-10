using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public class MapHelpers
    {
        private Dictionary<Vector2, MapNode> _mapNodes;

        public MapHelpers(Dictionary<Vector2, MapNode> mapNodes)
        {
            _mapNodes = mapNodes;
        }

        /// <summary>
        /// returnes null if already coaxial with the target
        /// </summary>
        /// <returns></returns>
        public List<Vector2> CanMove(Vector2 dir, float step,  Vector2 current, Vector2 final)
        {
            int i = 1;
            Vector2 axis = dir.AllPosVector();
            Vector2 temp = current;
            Vector2 dest = final * axis;
            if (dest == current*axis)
            {
                return null;
            }
            List<Vector2> path = new List<Vector2>();
            do
            {
                temp = current + i * dir * step;
                path.Add(temp);
                if (temp * axis == dest)
                {
                    return path;
                }
                i++;
            } while (CheckPosition(temp) == true);
            throw new System.Exception($"CANNOT MOVE to {dest} position");
        }

        private bool CheckPosition(Vector2 pos)
        {
            if (_mapNodes.TryGetValue(pos, out MapNode node) == false)
                return false;
            if(_mapNodes[pos].IsWalkable == false)
            {
                return false;
            }
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns> (Dir along Hor Axis, Dir along Vert Axis) </returns>
        public static (Vector2, Vector2) GetDirectionVectors(Vector2 from, Vector2 to)
        {
            Vector2 hor = new Vector2(Mathf.Sign(to.x - from.x), 0);
            Vector2 vert = new Vector2( 0,  Mathf.Sign(to.x - from.x));
            return (hor, vert);
        }
    }



    
}