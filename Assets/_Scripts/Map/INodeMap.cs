using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public interface INodeMap<T>
    {
        List<Node<T>> GetNeighbours(Node<T> node);
        Dictionary<Vector2, MapNode> GetNodes();
    }


}