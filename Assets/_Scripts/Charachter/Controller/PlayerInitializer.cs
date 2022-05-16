
using UnityEngine;
using Zenject;
namespace BomberGame
{

    public class PlayerInitializer : MonoBehaviour, IActor
    {
        public string ActorID;
        [SerializeField] private PlayerSettingsSO _settings;
        public bool SelfInit = false;
        [Space(10)]
        // health
        [SerializeField] private CharachterHealth _health;
        // view
        [SerializeField] private SpriteView _view;
        // movement
        [SerializeField] private CircleCaster _caster;
        private CharachterTileMover _mover;
        private InputMoveController _inputMoveController;

        private void Start()
        {
            if (SelfInit == true)
            {
                Init(_settings, ActorID);
                SetActive();
            }
        }


        public void Init(PlayerSettingsSO setting, string ID)
        {
            ActorID = ID;
            if (_settings == null)
            {
                Debug.Log("Settings not set");
                return;
            }
            _mover = new CharachterTileMover(new RaycastPositionValidator(_caster), _view,_settings.MovementSettings);
            _inputMoveController = new InputMoveController(_mover, _settings.InputMoveChannel, _view);
            _view?.Init(_settings.Sprites);
            _view?.Enable();

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

        }

        public void Kill()
        {

        }


    }
}