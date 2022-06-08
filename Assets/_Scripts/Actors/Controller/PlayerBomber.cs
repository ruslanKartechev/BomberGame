using UnityEngine;
using BomberGame.Health;
using Zenject;
using System.Collections;
namespace BomberGame
{
    [DefaultExecutionOrder(100)]
    public class PlayerBomber : IBomberActor
    {
        [SerializeField] private BomberSettings _settings;
        [SerializeField] private PlayerBomberSettings _playerSettings;
        [SerializeField] private Map _map;
        [SerializeField] private SpriteView _view;
        public bool SelfInit = false;
        [Space(10)]
        // health
        private HealthComponent _healthComponent;
        private PlayerDamagableAdaptor _damageAdaptor;
        private PlayerHealableAdaptor _healableAdaptor;
        private PlayerMaxHealthBuffsAdaptor _healthBuffAdaptor;
        // movement
        private Actor2DMapMover _mover;
        private PlayerMovementController _playerMovementController;
        private MovementMapAdaptor _movementMapAdaptor;
        private MovementBuffAdaptor _moveBuffAdaptor;
        // bomb placement
        private BombPoolGetter _bombGetter;
        private BombBuffer _bombBuffableAdaptor;
        private BombPlacementManager _bombingManager;
        private PlayerAttackController _bombInputController;
        // buffs
        private ActiveBuffContainer _buffsContainer;
        //
        [Inject] private ActorsContainer _actorContainer;

        private void Start()
        {
            if (SelfInit == true)
            {
                Init();
                SetActive();
                _actorContainer?.AddActor(this, _ID);

            }
        }

        private void OnDisable()
        {
            _playerMovementController.DisableMovement();
        }

        #region Init
        public override void Init()
        {
            if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            InitMovement();
            InitHealth();
            InitBombs();
            InitContainer();
            // view
            _view?.Enable();
        }

        private void InitMovement()
        {
            _mover = new Actor2DMapMover(new MapPositionValidator(_map), _view, _view, _settings.MovementSettings);
            _playerMovementController = new PlayerMovementController(_mover, _playerSettings.InputMoveChannel);
            _movementMapAdaptor = new MovementMapAdaptor(_map, this, _mover, _view.GetPostion());
            _mover.InitStartPosition(_view.GetPostion());
            ISpeedBuffVFX speedVFX = null;
            if (_entityComponents.TryGetValue(typeof(ISpeedBuffVFX), out object effect))
            {
                speedVFX = (ISpeedBuffVFX)effect;
            }
            _moveBuffAdaptor = new MovementBuffAdaptor(_mover, speedVFX);
        }

        private void InitHealth()
        {
            _healthComponent = new HealthComponent();
            _healthComponent.Init(_settings.Health);
            #region TryGetVFX
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
            #endregion
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
            _bombInputController = new PlayerAttackController(_playerSettings.AttackChannel, _bombingManager, _mover);
        }

        private void InitContainer()
        {
            _entityComponents.Add(typeof(IDamagable), _damageAdaptor);
            _entityComponents.Add(typeof(IHealable), _healableAdaptor);

            _entityComponents.Add(typeof(ISpriteView2D), _view);
            _entityComponents.Add(typeof(IHealthComponent), _healthComponent);

            _buffsContainer = new ActiveBuffContainer();
            _entityComponents.Add(typeof(ActiveBuffContainer), _buffsContainer);
            _entityComponents.Add(typeof(ISpeedBuffable), _moveBuffAdaptor);
            _entityComponents.Add(typeof(IMaxHealthBuffable), _healthBuffAdaptor);
            _entityComponents.Add(typeof(IBombBuffer), _bombBuffableAdaptor);
        }
        #endregion

        #region Actor
        public override void SetIdle()
        {
            _playerMovementController.DisableMovement();
        }

        public override void SetActive()
        {
            _playerMovementController.EnableMovement();
            _bombInputController.Enable();
        }

        #endregion
    }
}