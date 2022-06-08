using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class MapNode : Node<Vector2>
    {
        private INodeMap<Vector2> _map;
        public bool IsWalkable = true;
        public MapNode(Vector2 position, INodeMap<Vector2> map) : base(position)
        {
            _map = map;
        }

        public override List<Node<Vector2>> GetNeighbours()
        {
            return _map.GetNeighbours(this);
        }
    }


}