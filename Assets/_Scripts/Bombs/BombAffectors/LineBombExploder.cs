using System.Collections.Generic;
using UnityEngine;
using System;

namespace BomberGame.Bombs
{
    public class LineBombExploder2D : BombExploder<Vector2>, IExplosionRangeBuffable, IExplosionPiercingBuffable
    {
        public LineExplosionEffect ExplosionEffect;
        public float EffectDuration;
        public override List<ExplosionTarget<Vector2>> Explode()
        {
            List<ExplosionTarget<Vector2>> finalResult = new List<ExplosionTarget<Vector2>>();
            var centerpos = _caster.CheckActorsAt(_centerPosition);
            if(centerpos != null)
            {
                foreach(InteractableEntity actor in centerpos)
                {
                    finalResult.Add(new ExplosionTarget<Vector2>(actor, 1, _centerPosition));
                }
              
            }

            for (int i = 0; i < _castDirections.Count; i++)
            {
                List<ExplosionTarget<Vector2>> sideResult = CastSide(_castDirections[i]);

                finalResult.AddRange(sideResult);
            }
            return finalResult;
        }

        protected  List<ExplosionTarget<Vector2>> CastSide(Vector2 dir)
        {
            List<ExplosionTarget<Vector2>> obstacles = GetObstacles(_centerPosition, dir, _gridSize, _explosionLength, _piercing);
            int cellDepth = _explosionLength - obstacles.Count;
            List<ExplosionTarget<Vector2>> actors = GetActors(_centerPosition, dir, _gridSize, cellDepth);
            obstacles.AddRange(actors);
            return obstacles;
        }

        protected List<ExplosionTarget<Vector2>> GetObstacles(Vector2 start, Vector2 dir, float gridSize, float length, int depth)
        {
            List<ExplosionTarget<Vector2>> result = new List<ExplosionTarget<Vector2>>();
            int pierced = 0;
            Vector2 line_endPos = _centerPosition ;
            for (int i = 1; i <= length && pierced < depth; i++)
            {
                Vector2 pos = start + dir * gridSize * i;
                line_endPos = pos;
                try
                {
                    var obstacle = _caster.CheckObstacleAt(pos);
                    if (obstacle != null)
                    {
                        if (obstacle.EntityID == EntityIDs.FixedWall)
                        {
                            line_endPos = pos - dir * gridSize / 2;
                            float lineLength = ((line_endPos) - _centerPosition).magnitude;
                            ExplosionEffect.Play(_centerPosition, dir, lineLength, EffectDuration);
                            return result;
                        }
                        result.Add(new ExplosionTarget<Vector2>(obstacle,1, pos));
                        pierced++;
                    }
                }
                catch
                {
                    line_endPos = pos - dir * gridSize / 2;
                    float lineLength = ((line_endPos) - _centerPosition).magnitude;
                    ExplosionEffect.Play(_centerPosition, dir, lineLength , EffectDuration);
                    return result;
                }
            }
            float lineLen = (line_endPos - _centerPosition).magnitude;
            ExplosionEffect.Play(_centerPosition, dir, lineLen , EffectDuration);

            return result;
        }



        protected List<ExplosionTarget<Vector2>> GetActors(Vector2 start, Vector2 dir, float gridSize, float length)
        {
            List<ExplosionTarget<Vector2>> result = new List<ExplosionTarget<Vector2>>();
            for (int i = 1; i <= length; i++)
            {
                Vector2 position = start + dir * gridSize * i;
                try
                {
                    var actors = _caster.CheckActorsAt(position);
                    foreach (InteractableEntity actor in actors)
                    {
                        result.Add(new ExplosionTarget<Vector2>(actor, 1, _centerPosition));
                    }
                }
                catch
                {
                    Debug.Log("cannot check actor at position " + position);
                }
            }
            return result;
        }


        void IExplosionRangeBuffable.BuffMapRange(int gridLength)
        {
            _explosionLength = gridLength;
        }

        void IExplosionPiercingBuffable.BuffPiercingDepth(int piercingDepth)
        {
            _piercing = piercingDepth;
        }


    }
}