using UnityEngine;
namespace BomberGame
{
    public interface ITileMover<T>
    {
        T RealTimePosition();
        T ToPosition();
        T FromPosition();
    }
}