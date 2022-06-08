using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
namespace BomberGame.Bombs
{
    public class DefaultBombManager : Bomb
    {
        public event Action OnBombDone;

        [SerializeField] protected BombSettingsSO _settings;
        [SerializeField] protected BombExplosionSettings<Vector2> _explosionSettings;

        [SerializeField] protected BombDamageComponent _damager;
        [Space(10)]
        [SerializeField] protected BombCountDown _countEffect;
        [SerializeField] protected LineExplosionEffect _explosionEffect;
        protected LineBombExploder2D _exploder;
        protected ExplosionResultAffector<Vector2> _lineExplosionAffector;

        protected IObstacleMap<Vector2> _obstacleMap;
        protected IActorsMap<Vector2> _actorMap;
        protected Vector2 _myPosition;
        protected IBombView _view;

        protected bool _inited = false;

        public override void Init(Vector2 position, string placerID, IObstacleMap<Vector2> obstacleMap, IActorsMap<Vector2> actorMap)
        {
            _myPosition = position;
            gameObject.transform.position = position;
            _obstacleMap = obstacleMap;
            _actorMap = actorMap;
            _damager.DealerID = placerID;
            _exploder = new LineBombExploder2D();
            _exploder.Init(_settings, _explosionSettings, new BombMapCaster<Vector2>(_actorMap, _obstacleMap,1), _myPosition);
            _exploder.ExplosionEffect = _explosionEffect;
            _exploder.EffectDuration = _explosionSettings.Duration;
            _lineExplosionAffector = new ExplosionResultAffector<Vector2>(_damager, _damager);
            if(!_inited)
                InitComponents();
            _view.Show();
        }

        private void InitComponents()
        {
            _view = GetComponent<IBombView>();
            _entityComponents.Add(typeof(IExplosionRangeBuffable),_exploder);
            _entityComponents.Add(typeof(IExplosionPiercingBuffable), _exploder);
            _entityComponents.Add(typeof(IDamageBuffable), _damager);
            _damager.Init();
            _inited = true;
        }

        public override async void StartBomb()
        {
            _countEffect.StartCountdown(_settings.CountdownTime);
            await Task.Delay((int)(1000*_settings.CountdownTime));
            _countEffect?.OnExplode();
 
            List<ExplosionTarget<Vector2>> targets = _exploder.Explode();
            _lineExplosionAffector.HandleExplosionResult(targets);
            
            //await Task.Delay((int)(1000 * _explosionEffect.Duration));
            await Task.Delay((int)(1000 * _explosionSettings.Duration));
            _damager?.Refresh();
            OnBombDone?.Invoke();
        }

    }

}