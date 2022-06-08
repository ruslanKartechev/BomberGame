using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
namespace BomberGame.Bombs
{
    public abstract class BombExploder<T>
    {
        protected Vector2 _centerPosition;
        protected int _explosionLength;
        protected int _piercing;
        protected float _gridSize;
        protected BombExplosionSettings<T> _explosionSettings;
        protected List<T> _castDirections;
        protected IExplosionCaster<T> _caster;
        public event Action OnExplosion;
        public virtual void Init(BombSettingsSO settings, BombExplosionSettings<T> explosionSettings, IExplosionCaster<T> caster, Vector2 position)
        {
            _gridSize = settings.GridSize;
            _explosionLength = settings.StartExplosionLength;
            _piercing = settings.PiercingDepth;
            _centerPosition = position;
            _caster = caster;
            _explosionSettings = explosionSettings;
            _castDirections = _explosionSettings.CastDirections;
        }

        public abstract List<ExplosionTarget<T>> Explode();

    }
}