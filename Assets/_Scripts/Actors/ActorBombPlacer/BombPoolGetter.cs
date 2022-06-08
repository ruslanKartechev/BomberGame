using UnityEngine;
using BomberGame.Bombs;
namespace BomberGame
{
    public class BombPoolGetter : IBombGetter
    {
        private BombInventory _inventory;
        private BombPoolChannelSO _bombPoolChannel;

        public BombPoolGetter(BombInventory inventory, BombPoolChannelSO bombPoolChannel)
        {
            _inventory = inventory;
            _bombPoolChannel = bombPoolChannel;
        }

        public Bomb GetBomb()
        {
            Bomb b = TrGetBomb();
            if (b != null)
            {
                return b;
            }
            else
                throw new System.Exception("Cannot return bomb");
        }

        private Bomb TrGetBomb()
        {
            string id = GetIDfromInventory();
            if (id == "empty")
            {
                Debug.Log($"Bomb inventory is empty:");
                return null;
            }
            try
            {
                Bomb b = _bombPoolChannel.GetBombByID(id);
                return b;
            }
            catch
            {
                Debug.Log("Bomb Channel Did not return a bomb instance");
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
    }




}