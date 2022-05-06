using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public class PlayerBombPlacer : BombPlacerBase
    {
        [SerializeField] private CharachterBombPositioner _positioner;
        [Space(5)]
        [SerializeField] private BombColorer _colorer;
        private IBuffer _bombBuffer;
        [Space(10)]
        [SerializeField] private bool SelfInit = false;
        private CharachterInventory _inventory;

        private void Awake()
        {
            _bombBuffer = GetComponent<IBuffer>();
            if (_bombBuffer == null)
                Debug.Log("Bomb buffer not found");
            _inventory = GetComponent<CharachterInventory>();
        }

        private void Start()
        {
            if (SelfInit == true)
                Enable();
        }

        public override void Disable()
        {
            AttackChannel.Attack -= PlaceBomb;
        }

        public override void Enable()
        {
            AttackChannel.Attack += PlaceBomb;
        }

        public void PlaceBomb()
        {
            string id = _inventory.GetBomb();
            if (id != "empty")
            {
                GameObject b = _bombPrefabs.GetPrefab(id);
                b = Instantiate(b);
                _colorer?.ColorBomb(b);

                BombBase bomb = b.GetComponent<BombBase>();
                PlaceBomb(bomb);
                bomb.InitCoundown();
                _bombBuffer?.BuffBomb(b, _inventory.GetBombBuffs());

            }
            else
            {
                Debug.Log("returned empty");
            }
        }

        private void PlaceBomb(IPlaceable target)
        {
            if(_positioner!= null)
            {
                target.Place(_positioner.GetPosition());
            }
            else
            {
                target.Place(transform.position);
            }
        }
    }
}