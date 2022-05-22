using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    public class BombRaycaster : IBombCaster
    {
        private CircleCastSettings _settings;
        private float _gridSize = 2;
        public BombRaycaster(CircleCastSettings settings, float GridSize)
        {
            _settings = settings;
            _gridSize = GridSize;
        }

        public List<RaycastResult> CastDirection(Vector2 start, Vector2 dir, float gridLength)
        {
            float length = gridLength * _gridSize - _gridSize/2 - _settings.Radius;
            start += dir * _settings.Radius;
            RaycastHit2D[] hits = Physics2D.CircleCastAll(start, _settings.Radius, dir, length, _settings.Mask);
            List<RaycastResult> result = new List<RaycastResult>(hits.Length);
            foreach (RaycastHit2D h in hits)
            {
                if (h == true)
                    result.Add(new RaycastResult(h.collider.gameObject, h.distance));
            }
            result.Sort();
            return result;
        }

        public List<IAdaptableActor> GetActors(Vector2 start, Vector2 dir, float gridSize, float length)
        {
            throw new System.NotImplementedException();
        }

        public List<IAdaptableObstacle> GetObstacles(Vector2 start, Vector2 dir, float gridSize, float length, int depth)
        {
            throw new System.NotImplementedException();
        }
    }
}