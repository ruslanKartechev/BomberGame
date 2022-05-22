
using UnityEngine;
using BomberGame.Health;
namespace BomberGame
{
    [DefaultExecutionOrder(100)]
    public class AIPawn : MonoBehaviour, IActor
    {

        public string ActorID;
        [SerializeField] private AIPawnSettings _settings;
        [SerializeField] private Map _map;

        public bool SelfInit = false;
        [Space(10)]
        // AI
        private AIBrain _AIBrains;
        private AIMovement _AIMovement;
        private AIBomber _AIBomber;
        // health
        private PawnHealth _health;
        private PawnHealthWrapper _healthWrapper;
        [SerializeField] private DamageDetector _damageDetector;
        [SerializeField] private DamageEffectBase _damageEffect;
        // view
        [SerializeField] private SpriteView _view;
        // movement
        private CharachterPathMover _pahtMover;
        //private CharachterTileMover _mover;
        // bombs
        [SerializeField] private BombPlacer _bombPlacer;


        private void Start()
        {
            if (SelfInit == true)
            {
                Init(_settings, ActorID);
                SetActive();
                _AIBrains?.Enable();
            }
        }
        private void OnDisable()
        {
            _AIBrains?.Disable();
        }

        public void Init(PawnSettings setting, string ID)
        {
            ActorID = ID;
            if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            _view?.Init(_settings.Sprites);
            _view?.Enable();
            InitMovement();
            InitHealth();

            _bombPlacer?.Init(_settings.BombInventory, _settings.BombPrefabs, ActorID, _map);
            _settings.BombInventory?.Init();
      
            InitAI();
        }
        
        private void InitMovement()
        {
            MapPositionValidator mapPosValidator = new MapPositionValidator(_map);
            _pahtMover = new CharachterPathMover(mapPosValidator, _view, _view,_settings.MovementSettings, _settings._spawnSettings.StartPosition);
        }

        private void InitHealth()
        {
            _health = new PawnHealth();
            _health.Init(_settings.Health);
            _healthWrapper = new PawnHealthWrapper(_health, _settings._healthChannel, _damageEffect);
            _damageDetector?.Init(_healthWrapper, ActorID);
        }
        private void InitAI()
        {
            Debug.Log("init ai");
            _AIMovement = new AIMovement(_pahtMover, _map);
            _AIBrains = new AIBrain(_AIMovement,new AIBomber(), _map);
        }




        public void SetIdle()
        {
            _health?.DisableDamage();

        }


        public void SetActive()
        {
            _health?.EnableDamage();
            _damageDetector?.Enable();
        }


        public void Kill()
        {

        }

    }
}
