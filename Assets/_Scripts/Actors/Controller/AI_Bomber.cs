
using UnityEngine;
using BomberGame.Health;
using Zenject;
using System.Collections.Generic;
namespace BomberGame
{

    [DefaultExecutionOrder(100)]
    public class AI_Bomber : IBomberActor
    {
        public SpawnSettings SpawnSettings;
        [SerializeField] private BomberSettings _settings;
        [SerializeField] private AIBomberSettings _AISettings;
        [SerializeField] private Map _map;
        [SerializeField] private SpriteView _view;
        [Space(10)]
        public bool SelfInit = false;
        [Space(10)]
        // AI_settings
        private AIBrain _AIBrains;
        private AIMovement _AIMovement;
        private AIAttackController _AIBomber;
        // health
        private HealthComponent _healthComponent;
        private PlayerDamagableAdaptor _damageAdaptor;
        private PlayerHealableAdaptor _healableAdaptor;
        private PlayerMaxHealthBuffsAdaptor _healthBuffAdaptor;
        // movement
        private ActorPathMover _pahtMover;
        private MovementMapAdaptor _moveMapWrapper;
        private MovementBuffAdaptor _moveBuffAdaptor;
        // bombs
        private BombPoolGetter _bombGetter;
        private BombBuffer _bombBuffableAdaptor;
        private BombPlacementManager _bombingManager;
        // buffs
        private ActiveBuffContainer _buffsContainer;
        private void Start()
        {
            if (SelfInit == true)
            {
                Init();
                SetActive();
            }
        }

        private void OnDisable()
        {
            _AIBrains?.Disable();
            //_AIMovement.Stop();
        }

        #region Actor
        public override void SetIdle()
        {
            _AIBrains?.Disable();
        }

        public override void SetActive()
        {
            _AIBrains?.Enable();
        }
        #endregion


        #region Init
        public override void Spawn(SpawnSettings settings)
        {
            SpawnSettings = settings;
        }

        public override void Init()
        {
             if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            _view?.Enable();
            InitMovement();
            InitHealth();
            InitBombs();
            InitAI();
            InitComponentContainer();
        }

        private void InitMovement()
        {
            MapPositionValidator mapPosValidator = new MapPositionValidator(_map);
            _pahtMover = new ActorPathMover(mapPosValidator, _view, _view, _settings.MovementSettings);
            _pahtMover.InitStartPosition(SpawnSettings.StartPosition);
            _moveMapWrapper = new MovementMapAdaptor(_map, this, _pahtMover, SpawnSettings.StartPosition);
            ISpeedBuffVFX speedVFX = null;
            if (_entityComponents.TryGetValue(typeof(ISpeedBuffVFX), out object effect))
            {
                speedVFX = (ISpeedBuffVFX)effect;
            }
            _moveBuffAdaptor = new MovementBuffAdaptor(_pahtMover, speedVFX);
        }

        private void InitHealth()
        {
            _healthComponent = new HealthComponent();
            _healthComponent.Init(_settings.Health);
            IDamageVFX damageEffect = null;
            if (_entityComponents.TryGetValue(typeof(IDamageVFX), out object effect))
            {
                damageEffect = (IDamageVFX)effect;
            }
            IHealingVFX healingEffect = null;
            if (_entityComponents.TryGetValue(typeof(IHealingVFX), out object effect2))
            {
                healingEffect = (IHealingVFX)effect2;
            }
            IMaxHealthBuffVFX maxHealthVFX = null;
            if (_entityComponents.TryGetValue(typeof(IMaxHealthBuffVFX), out object effect3))
            {
                maxHealthVFX = (IMaxHealthBuffVFX)effect2;
            }

            _damageAdaptor = new PlayerDamagableAdaptor(_ID, _healthComponent, damageEffect);
            _damageAdaptor.AllowSelfDamage = true;
            _healableAdaptor = new PlayerHealableAdaptor(_healthComponent, healingEffect);
            _healthBuffAdaptor = new PlayerMaxHealthBuffsAdaptor(_healthComponent, _settings.Health, maxHealthVFX);
        }

        private void InitBombs()
        {
            _settings.BombInventory?.Init();
            _settings.BombInventory?.AddItem(_settings.BombInventory.DebugAddItemID, _settings.BombInventory.DebugAddItemCount);
            _bombGetter = new BombPoolGetter(_settings.BombInventory, _settings._bombChannel);
            _bombBuffableAdaptor = new BombBuffer();
            _bombingManager = new BombPlacementManager(_bombGetter, _bombBuffableAdaptor, _map, _map, _map, _ID);
            //_bombInputController = new PlayerAttackController(_settings.AttackChannel, _bombingManager, _mover);
        }

        private void InitAI()
        {
            _AIMovement = new AIMovement(_pahtMover, _map);
            _AIBrains = new AIBrain(_AIMovement,new AIAttackController(), _map);
        }

        private void InitComponentContainer()
        {
            _entityComponents.Add(typeof(ISpriteView2D), _view);
            _entityComponents.Add(typeof(IHealthComponent), _healthComponent);
            _entityComponents.Add(typeof(IDamagable), _damageAdaptor);
            _entityComponents.Add(typeof(IHealable), _healableAdaptor);

            _buffsContainer = new ActiveBuffContainer();
            _entityComponents.Add(typeof(ActiveBuffContainer), _buffsContainer);
            _entityComponents.Add(typeof(ISpeedBuffable), _moveBuffAdaptor);
            _entityComponents.Add(typeof(IMaxHealthBuffable), _healthBuffAdaptor);
            _entityComponents.Add(typeof(IBombBuffer), _bombBuffableAdaptor);
        }
        #endregion




    }
}
