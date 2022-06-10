using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace BomberGame
{
    public abstract class PathFindStrategy<T>
    {
        public abstract Task<WalkPath<T>>  GetPathAsync(Vector2 start, Vector2 end, CancellationToken token);
        public abstract WalkPath<T> GetPathSync(Vector2 start, Vector2 end);
    }
    
    public class WalkPath<T>
    {
        public List<T> Nodes;
        public int Length;

        public WalkPath(List<T> nodes)
        {
            Nodes = nodes;
            Length = nodes.Count;
        }

    }
    public class WalkPathComparer<T> : Comparer<WalkPath<T>>
    {
        //public int Compare(WalkPath<T> x, WalkPath<T> y)
        //{
        //    x.Length 
        //}
        public override int Compare(WalkPath<T> x, WalkPath<T> y)
        {
            return x.Length.CompareTo(y.Length);
        }
    }




}