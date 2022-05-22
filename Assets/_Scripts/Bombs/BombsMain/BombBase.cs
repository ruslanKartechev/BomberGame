using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
namespace BomberGame
{
    public abstract class BombBase : MonoBehaviour
    {
        protected Vector3 _myPosition;
        protected int _explosionLength;
        protected int _piercing;
        protected float _gridSize;
        protected List<Vector2> _castDirections;
        protected IBombCaster _caster;
        protected IBombOnTargetEffect _effectOnTargets;

        public virtual void Init(BombSettingsSO settings, IBombCaster caster, Vector2 position, IBombOnTargetEffect effectOnTargets)
        {
            _gridSize = settings.GridSize;
            _explosionLength = settings.StartExplosionLength;
            _piercing = settings.PiercingDepth;
            _castDirections = settings.CastDirections;
            _myPosition = position;
            _effectOnTargets = effectOnTargets;
            _caster = caster;
        }

        public abstract List<ExplosionPositions> Explode();



    }
}