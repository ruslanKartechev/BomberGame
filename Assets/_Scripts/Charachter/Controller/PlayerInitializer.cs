
using UnityEngine;
using Zenject;
namespace BomberGame
{

    public class PlayerInitializer : MonoBehaviour, IPlayer
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
        }

        private void Start()
        {
            if (SelfInit == true)
            {
                if (AutoRandom)
                {
                    PlayerID = "player_" + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9);
                }
                Init(_settings, PlayerID);
                SetActive();
            }
        }


        public void Init(PlayerSettingsSO setting, string ID)
        {
            if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            if (_mover != null)
            {
                _mover.Init(_settings.GridSnapTime);
            }
            if (_health != null)
            {
                _health.Init(_settings.StartHealth);
                _health.CharacterID = PlayerID;
            }

            if (_inventory != null)
            {
                BombInventory bombInv = ScriptableObject.Instantiate<BombInventory>(_settings.BombInventory);
                BuffInventory buffInv = ScriptableObject.Instantiate<BuffInventory>(_settings.BuffInventory);
                _inventory.Init(bombInv, buffInv);
            }
            PlayerID = ID;
        }

        public void SetIdle()
        {
            _mover.DisableMovement();
            _health.DisableDamage();
            _bombPlacer.Disable();
        }

        public void SetActive()
        {
            _mover.EnableMovement();
            _health.EnableDamage();
            _bombPlacer.Enable();
        }

        public void Kill()
        {

        }
    }
}