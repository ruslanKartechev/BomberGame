
using UnityEngine;
using BomberGame.Health;

namespace BomberGame
{

    [DefaultExecutionOrder(100)]

    public class ControllablePawn : MonoBehaviour, IActor
    {
        public string ActorID;
        [SerializeField] private ControllablePawnSettings _settings;
        [SerializeField] private Map _map;
        public bool SelfInit = false;
        [Space(10)]
        // health
        private PawnHealth _health;
        private PawnHealthWrapper _healthWrapper;
        [SerializeField] private DamageDetector _damageDetector;
        [SerializeField] private DamageEffectBase _damageEffect;
        // view
        [SerializeField] private SpriteView _view;
        // movement
        //[SerializeField] private CircleCaster _caster;
        private CharachterTileMover _mover;
        private InputMoveController _inputMoveController;
        // bombs
        [SerializeField] private BombPlacer _bombPlacer;
        private InputBomber _bomber;

        private void Start()
        {
            if (SelfInit == true)
            {
                Init(_settings, ActorID);
                SetActive();
            }
        }


        public void Init(PawnSettings setting, string ID)
        {
            ActorID = ID;
            if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            _mover = new CharachterTileMover(new MapPositionValidator(_map) , _view,_settings.MovementSettings, _view.GetPostion());
            _inputMoveController = new InputMoveController(_mover, _settings.InputMoveChannel, _view);
            _view?.Init(_settings.Sprites);
            _view?.Enable();

            _bombPlacer?.Init(_settings.BombInventory, _settings.BombPrefabs, ActorID,_map);
            _settings.BombInventory?.Init();
            _bomber = new InputBomber(_settings.AttackChannel,_bombPlacer,_mover);

            _health = new PawnHealth();
            _health.Init(_settings.Health);
            _healthWrapper = new PawnHealthWrapper(_health,_settings._healthChannel, _damageEffect);
            _damageDetector?.Init(_healthWrapper, ActorID);

        }

        public void SetIdle()
        {
            _health?.DisableDamage();
            _inputMoveController?.DisableMovement();
        }


        public void SetActive()
        {
            _health?.EnableDamage();
            _inputMoveController?.EnableMovement();
            _bomber?.Enable();
            _damageDetector?.Enable();

        }


        public void Kill()
        {

        }


    }
}