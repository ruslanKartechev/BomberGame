using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public class AttackStart_GetToClosestPoint
    {
        private int MaxAttackDistance;
        private float _step;
        private INodeMap<Vector2> _nodeMap;

        public AttackStart_GetToClosestPoint(int maxAttackDistance, INodeMap<Vector2> nodeMap)
        {
            _nodeMap = nodeMap;
            MaxAttackDistance = maxAttackDistance;
            _step = _nodeMap.GetGridSize();
        }

        public List<Vector2> GetPath(Vector2 myPos, Vector2 enemyPos)
        {
            var points = GetPointsAroundTarget(myPos, enemyPos);
            points = CleanPointsList(points);
            var finalPath =  GetShortestPath(myPos, points);
            return finalPath;
        }

        private List<Vector2> GetPointsAroundTarget(Vector2 myPos, Vector2 enemyPos)
        {
            var directions = MapHelpers.GetDirectionVectors(enemyPos, myPos);
            Vector2 dir_x = directions.Item1;
            Vector2 dir_y = directions.Item2 ;
            List<Vector2> points = new List<Vector2>((MaxAttackDistance - 1)*2);
            for(int i = 0; i < MaxAttackDistance - 1; i++)
            {
                var v = enemyPos + dir_x * (i + 1) * _step;
                points.Add(v);
                v = enemyPos + dir_y * (i + 1) * _step;
                points.Add(v);
            }
            return points;
        }

        private List<Vector2> CleanPointsList(List<Vector2> list)
        {
            var mapNodes = _nodeMap.GetNodes();
            for(int i = list.Count-1; i >= 0; i--)
            {
                if(!mapNodes.TryGetValue(list[i], out MapNode node))
                {
                    list.RemoveAt(i);
                    continue;
                }
                else if (node.IsWalkable == false)
                    list.RemoveAt(i);
            }
            return list;
        }


        private List<Vector2> GetShortestPath(Vector2 startPos, List<Vector2> points)
        {
            List<WalkPath<Vector2>> options = new List<WalkPath<Vector2>>();
            foreach (Vector2 p in points)
            {
                try
                {
                    options.Add(TryGetPath(startPos, p));
                }
                catch { Debug.Log("Cannot build path"); }
            }
            if(options.Count > 0)
            {
                options.Sort(new WalkPathComparer<Vector2>());
                return options[0].Nodes;
            }
            return null;
        }

        private WalkPath<Vector2> TryGetPath(Vector2 start, Vector2 end)
        {
            var startAlg = new AStartStrategy(_nodeMap);
            return startAlg.GetPathSync(start, end);
        }


    }


   

}