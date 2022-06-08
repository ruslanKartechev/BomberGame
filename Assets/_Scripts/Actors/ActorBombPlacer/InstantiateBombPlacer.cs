using UnityEngine;
using BomberGame.Bombs;
namespace BomberGame
{
    public class InstantiateBombPlacer : MonoBehaviour, IBombPlacer
    {
        private BombInventory _inventory;
        private BombsPrefabContainer _bombPrefabs;
        private string _actorID;
        private Map _map;

        public void Init(BombInventory inventory, BombsPrefabContainer bombs, string _actorID, Map map)
        {
            _inventory = inventory;
            _bombPrefabs = bombs;
            this._actorID = _actorID;
            _map = map;
        }

        public void PlaceBomb(Vector2 position)
        {
            Bomb b = TryGetBomb();
            if(b != null)
            {
                b.Init(position, _actorID, _map, _map);
                b.StartBomb();
                
            }

        }

        private Bomb TryGetBomb()
        {
            string id = GetIDfromInventory();
            if (id == "empty")
            {
                Debug.Log($"Bomb inventory is empty: {_actorID} ");
                return null;
            }
            try
            {
                Bomb b =_bombPrefabs.GetPrefab(id);
                b = Instantiate(b);
                return b;
            }
            catch
            {
                return null;
            }
        }

        private string GetIDfromInventory()
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

}