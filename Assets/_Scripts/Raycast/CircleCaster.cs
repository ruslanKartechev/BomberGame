using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class CircleCaster : MonoBehaviour
    {
        public RaycastHit2D _lastHit;
        [Space(10)]
        [HideInInspector] public float Distance;
        [Space(10)]
        public float AdditionalDistance;
        public float CircleCastRadius;
        public LayerMask Mask;
        
        public void InitParams(float distance, LayerMask mask)
        {
            Distance = distance;
            Mask = mask;
        }

        public void Raycast(Vector3 origin, Vector3 dir)
        {
            _lastHit = Physics2D.CircleCast(origin, CircleCastRadius, dir, Distance+ AdditionalDistance, Mask);
        }
        public List<RaycastHit2D> RaycastAll(Vector3 origin, Vector3 dir)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(origin, CircleCastRadius, dir, Distance + AdditionalDistance, Mask);
            List<RaycastHit2D> hitList = new List<RaycastHit2D>(hits.Length);
            hitList.AddRange(hits);
            return hitList;
        }

    }
}