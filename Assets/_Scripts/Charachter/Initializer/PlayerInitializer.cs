
using UnityEngine;

namespace BomberGame
{
    public class PlayerInitializer : MonoBehaviour
    {
        public string PlayerID;
        public bool AutoRandom = true;
        [Space(10)]
        [SerializeField] private PlayerSettingsSO _settings;
        public bool SelfInit = false;
        [Space(10)]
        [SerializeField] private CharachterMoverBase _mover;
        [SerializeField] private CharachterHealth _health;
        [SerializeField] private CharachterViewBase _view;
        [SerializeField] private PlayerInventoryManager _inventory;
        [SerializeField] private BombPlacerBase _bombPlacer;
        [SerializeField] private BombBuffApplier _buffer;
        private void Awake()
        {
            if (_mover == null)
                _mover = GetComponent<CharachterMoverBase>();
            if (_view == null)
                _view = GetComponent<CharachterViewBase>();
            if (_health == null)
                _health = GetComponent<CharachterHealth>();
            if (_inventory == null)
                _inventory = GetComponent<PlayerInventoryManager>();
            if (_bombPlacer == null)
                _bombPlacer = GetComponent<BombPlacerBase>();
            if (_buffer == null)
                _buffer = GetComponent<BombBuffApplier>();
            if (AutoRandom)
            {
                PlayerID = "player_" + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9);
            }
        }

        private void Start()
        {
            if(SelfInit == true)
            {
                InitPlayer();
                StartPlayer();
            }
        }

        public void SetSettings(PlayerSettingsSO settings)
        {
            _settings = settings;
        } 

        public void InitPlayer()
        {
            if(_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            if(_mover != null)
            {
                _mover.Init(_settings.GridSnapTime);
                _mover.InputMoveChannel = _settings.MoveChannel;
            }
            if(_health != null)
            {
                _health._channel = _settings.HealthUIChannel;
                _health.Init(_settings.StartHealth);
                _health.CharacterID = PlayerID;
            }

            if(_inventory != null)
            {
                BombInventory bombInv = ScriptableObject.Instantiate<BombInventory>(_settings.BombInventory);
                BuffInventory buffInv = ScriptableObject.Instantiate<BuffInventory>(_settings.BuffInventory);
                _inventory.Init(bombInv, buffInv);
            }
            if (_buffer)
            {
                _buffer._Buffs = _settings.BombBuffers;
            }
            
            if (_bombPlacer)
            {
                _bombPlacer.AttackChannel = _settings.AttackChannel;
                _bombPlacer.CharachterID = PlayerID;
            }
            if (_view)
            {
                _view.Sprites = _settings.Sprites;
            }

        }

        public void StartPlayer()
        {
            _mover.EnableMovement();
            _health.EnableDamage();
            _bombPlacer.Enable();

        }
        
    }
}