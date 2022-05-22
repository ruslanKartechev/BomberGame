using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{

    public class BombPlacer : MonoBehaviour, IBombPlacer
    {
        private BombInventory _inventory;
        private BombsPrefabs _bombPrefabs;
        private string _id;
        private Map _map;

        public void Init(BombInventory inventory, BombsPrefabs bombs, string id, Map map)
        {
            _inventory = inventory;
            _bombPrefabs = bombs;
            _id = id;
            _map = map;
        }

        public void PlaceBomb(Vector3 position)
        {
            string id = GetBomb();
            if (id != "empty")
            {
                BombManager b = _bombPrefabs.GetPrefab(id);
                b = Instantiate(b);
                b.Init(position, _id, _map);
                b.StartBomb();
                //_bombBuffer?.BuffBomb(b, _inventory.GetBombBuffs());
            }
        }

        public string GetBomb()
        {
            string id = _inventory.GetCurrentItemID();
            if (_inventory.TakeItem(id, 1) == true)
            {
                return id;
            }
            else
            {
                return "empty";
            }
        }

        public void OnExplode()
        {

        }

    }



    public class InputBomber
    {
        private InputAttackChannelSO _channel;
        private IBombPlacer _placer;
        private ITileMover _mover;
        public InputBomber(InputAttackChannelSO _attackChannel, IBombPlacer placer, ITileMover mover)
        {
            _channel = _attackChannel;
            _placer = placer;
            _mover = mover;
        }

        public void Enable()
        {
            _channel.Attack += Attack;
        }

        public void Disable()
        {
            _channel.Attack -= Attack;
        }

        public void Attack()
        {
            Vector3 position = _mover.GetPosition();
            _placer.PlaceBomb(position);
        }
    }

}