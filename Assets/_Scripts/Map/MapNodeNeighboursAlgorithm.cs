using System.Collections.Generic;
namespace BomberGame
{
    public abstract class MapNodeNeighboursAlgorithm<T>
    {
        public abstract List<Node<T>> GetNeighbours(T pos, Dictionary<T, MapNode> nodes, float gridSize);
    }


}