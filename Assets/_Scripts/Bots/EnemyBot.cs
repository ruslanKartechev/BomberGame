using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BomberGame
{
    public enum BotStates { Idle, Active, Dead}

    public class EnemyBot : MonoBehaviour
    {
        public string BotID;
        public bool AutoRandom = true;
        [Space(10)]

        private BotStates _state;
        public BotStates State { get { return _state; } }
        [Header("debug only")]
        [SerializeField] private BotSettings _settings;
        public bool SelfInit = false;
        [Space(10)]
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private EnemyHealth _health;
        [SerializeField] private BotView _view;
        [SerializeField] private BotBombPlacer _bombPlacer;
        [SerializeField] private BotInventoryManager _inventory;

        private void Awake()
        {
            if (_mover == null)
                _mover = GetComponent<EnemyMover>();
            if (_view == null)
                _view = GetComponent<BotView>();
            if (_health == null)
                _health = GetComponent<EnemyHealth>();
            if (_bombPlacer == null)
                _bombPlacer = GetComponent<BotBombPlacer>();
            if (_inventory == null)
                _inventory = GetComponent<BotInventoryManager>();
            if (AutoRandom)
            {
                BotID = "bot_" + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9) + UnityEngine.Random.Range(0, 9) ;
            }
        }

        private void Start()
        {
            if (SelfInit)
            {
                Init(_settings) ;
                SetState(BotStates.Active);
            }
        }

        public void Init(BotSettings settings)
        {
            _settings = settings;
            _mover?.Init(_settings.GridSnapTime);
            _view?.Init(_mover, _settings.Sprites);
            if(_bombPlacer != null)
            {
                _bombPlacer.Init(_mover, _settings.BombPrefabs, _settings.TimeMin, _settings.TimeMax);
                _bombPlacer.ChrachterID = BotID;
            }
            if (_health != null)
            {
                _health.OnDeath += OnDeath;
                _health.Init(_settings.StartHealth);
                _health.CharacterID = BotID;
            }
            if(_inventory != null)
            {
                BombInventory bombInv = ScriptableObject.Instantiate<BombInventory>(_settings.BombInventory);
                BuffInventory buffInv = ScriptableObject.Instantiate<BuffInventory>(_settings.BuffInventory);
                _inventory.Init(bombInv, buffInv);
            }

        }


        public void SetState(BotStates state)
        {
            _state = state;
            switch (state)
            {
                case BotStates.Idle:
                    _mover.Disable();
                    _health.DisableDamage();
                    _bombPlacer.Disable();
                    break;
                case BotStates.Active:
                    _mover.Enable();
                    _health.EnableDamage();
                    _bombPlacer.Enable();
                    break;
                case BotStates.Dead:
                    _mover.Disable();
                    _health.DisableDamage();
                    _bombPlacer.Disable();
                    break;
            }
        }

        private void OnDeath()
        {
            SetState(BotStates.Dead);
        }

    }

}