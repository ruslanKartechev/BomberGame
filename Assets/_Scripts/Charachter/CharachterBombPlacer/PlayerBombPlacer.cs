using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public class PlayerBombPlacer : BombPlacerBase
    {
        [SerializeField] private InventorySourceBase _inventorySource;
        [Space(5)]
        [SerializeField] private BombsPrefabs _bombPrefabs;
        [Space(5)]
        [SerializeField] private InputAttackChannelSO _attackChannel;
        [Space(5)]
        [SerializeField] private CharachterBombPositioner _positioner;
        [Space(5)]
        [SerializeField] private BombColorer _colorer;
        [Space(10)]
        [SerializeField] private bool SelfInit = false;

        private InventoryBase _bombInventory; 
        private IBombBuffer _buffer;
        private void Start()
        {
            _bombInventory = _inventorySource.GetBombsInventory();
            _bombInventory.Init();
            if (SelfInit == true)
                Enable();

            _buffer = GetComponent<IBombBuffer>();
            if (_buffer == null)
                Debug.Log("Bomb buffer not found");
        }

        public override void Disable()
        {
            _attackChannel.Attack -= PlaceBomb;
        }

        public override void Enable()
        {
            _attackChannel.Attack += PlaceBomb;
        }

        public override void PlaceBomb()
        {
            string id = _bombInventory.GetCurrentItem();
            GameObject b =  _bombPrefabs.GetPrefab(id);
            b = Instantiate(b);
            _colorer?.ColorBomb(b);
          

            BombBase bomb = b.GetComponent<BombBase>();
            PlaceBomb(bomb);
            bomb.InitCoundown();
            _buffer?.BuffBomb(b);
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