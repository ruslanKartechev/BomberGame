using System.Collections.Generic;
namespace BomberGame
{
    public abstract class Node<T>
    {
        public T Value { get; protected set; }
        public Node(T value)
        {
            Value = value;
        }
        public abstract List<Node<T>> GetNeighbours();
    }
}
