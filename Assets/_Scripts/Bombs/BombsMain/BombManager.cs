using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;

namespace BomberGame
{
    public class BombManager : MonoBehaviour
    {
        [SerializeField] BombBase _bomb;
        [SerializeField] private BombSettingsSO _settings;
        [SerializeField] private BombDamage _damage;

        [SerializeField] private ExplosionEffect _effect;
        [SerializeField] private BombCountDown _countEffect;
        private Map _map;
        private Vector2 _myPosition;

        public void Init(Vector2 position, string placerID,Map map)
        {
            _myPosition = position;
            _map = map;
            _damage.DealerID = placerID;
            transform.position = _myPosition;
        }

        public async void StartBomb()
        {
            _bomb.Init(_settings, new BombMapCaster(_map, _map), _myPosition, _damage);
            _countEffect.StartCountdown(_settings.CountdownTime);
            await Task.Delay((int)(1000*_settings.CountdownTime));
            List<ExplosionPositions> result = _bomb.Explode();
            if(result != null)
            {
                _effect.Play(_myPosition,result);
            }
            await Task.Delay((int)(1000 * _effect.Duration));
            gameObject.SetActive(false);
        }
    }
}