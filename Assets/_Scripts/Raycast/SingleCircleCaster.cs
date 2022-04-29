using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class SingleCircleCaster : MonoBehaviour
    {
        public RaycastHit2D _lastHit;
        [Space(10)]
        public float Distance;
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
            //Raycast(Vector2 origin, Vector2 direction, float distance, int layerMask);
            
            _lastHit = Physics2D.CircleCast(origin, CircleCastRadius, dir, Distance+ AdditionalDistance, Mask);
        }

    }
}