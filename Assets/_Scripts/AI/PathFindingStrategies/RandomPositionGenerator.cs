using UnityEngine;
using System.Linq;
using System.Collections.Generic;
namespace BomberGame
{
    public class RandomPositionGenerator
    {
        public INodeMap<Vector2> _nodeMap;
        public RandomPositionGenerator(INodeMap<Vector2> map)
        {
            _nodeMap = map;
        }

        public Vector2 GetPosition(Vector2 currentPosition)
        {
            Vector2 position = currentPosition;
            int i = 0;
            var nodes = _nodeMap.GetNodes();
            List<Vector2> mapPositions = new List<Vector2>(nodes.Keys);
            do
            {
                int index = UnityEngine.Random.Range(0, mapPositions.Count);
                position = mapPositions[index];
                i++;
            } while ((nodes[position].IsWalkable == false || position == currentPosition )&& i <20);
            return position;
        }

        public Vector2 GetPositionLimitedScope(Vector2 currentPosition, int maxNumberOfNeighbours)
        {
            Vector2 position = currentPosition;
            int i = 0;
            var nodes = _nodeMap.GetNodes();
            List<Node<Vector2>> mapPositions = LimitScope(currentPosition, nodes, maxNumberOfNeighbours).ToList();
            do
            {
                int index = UnityEngine.Random.Range(0, mapPositions.Count);
                position = mapPositions[index].Value;
                i++;
            } while ((nodes[position].IsWalkable == false || position == currentPosition) && i < 20);
            return position;
        }

        private HashSet<Node<Vector2>> LimitScope(Vector2 center, Dictionary<Vector2, MapNode> nodes, int maxNumberOfNeighbours)
        {
            HashSet<Node<Vector2>> finalScope = new HashSet<Node<Vector2>>();
            HashSet<Node<Vector2>> dummySet = new HashSet<Node<Vector2>>();
            dummySet.Add(nodes[center]);
            for(int i=0; i < maxNumberOfNeighbours; i++)
            {
                dummySet = AddNeighbours(dummySet);
                finalScope.UnionWith(dummySet);
            }
            Debug.Log($"Limited scope to {finalScope.Count} NODES");
            return finalScope;
        }

        private HashSet<Node<Vector2>> AddNeighbours(HashSet<Node<Vector2>> nodes)
        {
            HashSet<Node<Vector2>> neighboursSet = new HashSet<Node<Vector2>>();
            foreach (Node<Vector2> v in nodes)
            {
                foreach(Node<Vector2> n in v.GetNeighbours())
                {
                    neighboursSet.Add(n);
                }
            }
            return neighboursSet;
        }

    }

}