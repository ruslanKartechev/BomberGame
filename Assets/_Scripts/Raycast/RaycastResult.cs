
using UnityEngine;
using System;
namespace BomberGame
{
    public class RaycastResult : IComparable<RaycastResult>
    {
        public float Distance;
        public GameObject GO;
        public RaycastResult() { }
        public RaycastResult(GameObject go, float distance)
        {
            GO = go;
            Distance = distance;
        }
        public int CompareTo(RaycastResult other)
        {
            return Distance.CompareTo(other.Distance);
        }
    }
}