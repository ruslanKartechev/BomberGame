using System.Collections.Generic;
using UnityEngine;
using System;

namespace BomberGame
{

    public class BasicBomb : BombBase, ILengthBuffable, IPierceBuffable
    {
        public override List<ExplosionPositions> Explode()
        {
            List<ExplosionPositions> results = new List<ExplosionPositions>(4);
            for(int i = 0; i < _castDirections.Count; i++)
            {
                float length = CastSide(_castDirections[i]);
                ExplosionPositions sideResult = new ExplosionPositions();
                sideResult.Direction = _castDirections[i];
                sideResult.Length = length;
                results.Add(sideResult);
            }
            return results;
        }

        protected virtual float CastSide(Vector2 dir)
        {
            List<IAdaptableObstacle> obstacles = _caster.GetObstacles(_myPosition, dir, _gridSize, _explosionLength, _piercing);
            int cellDepth = _explosionLength - obstacles.Count;
            float distace = _gridSize * cellDepth;
            List<IAdaptableActor> actors = _caster.GetActors(transform.position, dir, _gridSize, cellDepth);
            //Debug.Log($"casted for {obstacles.Count} obstacles");
            //Debug.Log($"casted for {actors.Count} actors");

            _effectOnTargets?.ApplyActors(actors);
            _effectOnTargets?.ApplyObstacles(obstacles);

            return distace;
        }

        #region Buffs
        public virtual void BuffLength(int length)
        {
            _explosionLength = length;
        }

        public virtual void BuffPierce(int pierce)
        {
            _piercing = pierce;
        }
        #endregion
    }
}