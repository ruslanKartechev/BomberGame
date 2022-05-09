using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace BomberGame.UI
{
    public class BombMenuManager : BombMenuBase
    {
        [Inject] private BombSpriteByID _bombSprites;
        [SerializeField] private BombMenuUI _ui;
        private BombInventory _inventory;
        private List<string> _shownItems = new List<string>();
        private string _highlighted;

        private void Awake()
        {
            _ui.Init();
        }

        public override void SetInventory(BombInventory inventory)
        {
            _inventory = inventory;
        }

        public override void ShowMenu()
        {
            Debug.Log("show menu");
        }

        public override void HideMenu()
        {
            Debug.Log("Hide menu");
        }

        public override void UpdateView()
        {
            foreach (string id in _inventory.ItemCount.Keys)
            {
                int count = _inventory.ItemCount[id];
                if (_shownItems.Contains(id))
                {
                    _ui.UpdateText(id, count.ToString());
                    if(count <=0 )
                    {
                        _ui.HideBlock(id);
                        _shownItems.Remove(id);
                    }
                }
                else
                {
                    if(count > 0)
                    {
                        Sprite sprite = _bombSprites.GetSprite(id);
                        if (sprite == null)
                        {
                            Debug.Log($"sprite {id} was not found");
                            return;
                        }
                        _ui.SetBlock(sprite, count.ToString(), id);
                        _ui.Highlight(id);
                        _shownItems.Add(id);
                    }
                }
            }
            string currentItem = _inventory.CurrentID;
            if(currentItem != _highlighted && _shownItems.Contains(currentItem))
            {
                SetCurrent(currentItem);
            }
        }

        public override void SetCurrent(string id)
        {
            _ui.Highlight(id);
        }

        
    }
}