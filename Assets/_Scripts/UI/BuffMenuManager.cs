using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BomberGame;
namespace BomberGame.UI
{
    public class BuffMenuManager : BuffMenuBase
    {
        [SerializeField] private BuffMenuUI _ui;
        [SerializeField] private BuffSpriteByID _sprites;
        private BuffInventory _inventory;
        private Dictionary<string, int> _shown = new Dictionary<string, int>();

        private void Awake()
        {
            _ui.Init();
            _ui.HideAll();
        }

        public override void SetInventory(BuffInventory inventory)
        {
            _inventory = inventory;
        }

        public override void UpdateView()
        {
            foreach(string id in _inventory.ItemCount.Keys)
            {
                string fullID = id + "_" + _inventory.ItemCount[id].ToString();
                if (_shown.ContainsKey(id) == false)
                {
                    _ui.AddBlock(_sprites.GetSprite(fullID), id);
                    _shown.Add(id, _inventory.ItemCount[id]);
                }
                else
                {
                    if(_shown[id] != _inventory.ItemCount[id])
                    {
                        _ui.ChangeBlock(_sprites.GetSprite(fullID), id);
                    }
                }
            }
        }
    }
}