using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
namespace BomberGame
{
    public abstract class PathFindStrategy
    {
        public abstract  Task<List<Vector2>> GetPath(Vector2 start, Vector2 end);
        public abstract void Stop();
    }
}