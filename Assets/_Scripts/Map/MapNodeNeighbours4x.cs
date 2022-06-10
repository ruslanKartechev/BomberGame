using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class MapNodeNeighbours4x : MapNodeNeighboursAlgorithm<Vector2>
    {
        private float _gridSize;
        private Dictionary<Vector2, MapNode> _mapNodes;

        public override List<Node<Vector2>> GetNeighbours(Vector2 pos, Dictionary<Vector2, MapNode> nodes, float gridSize)
        {
            _mapNodes = nodes;
            _gridSize = gridSize;
            List<Node<Vector2>> others = new List<Node<Vector2>>();
            Node<Vector2> l = GetLeftCell(pos);
            Node<Vector2> r = GetRightCell(pos);
            Node<Vector2> u = GetUpCell(pos);
            Node<Vector2> d = GetDownCell(pos);
            if (l != null)
                others.Add(l);
            if (r != null)
                others.Add(r);
            if (u != null)
                others.Add(u);
            if (d != null)
                others.Add(d);
            return others;
        }

        private Node<Vector2> GetLeftCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.left * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetRightCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.right * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetUpCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.up * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetDownCell(Vector2 gridPosition)
        {
            gridPosition -= Vector2.up * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> TryGetCell(Vector2 gridPosition)
        {
            MapNode res = null;
            if (_mapNodes.TryGetValue(gridPosition, out res))
            {
                if (res.IsWalkable == true)
                    return res;
            }
            return null;
        }
    }


}