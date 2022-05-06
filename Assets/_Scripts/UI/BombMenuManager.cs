using System.Collections.Generic;
using UnityEngine;
using BomberGame;
namespace BomberGame.UI
{
    public class BombMenuManager : MonoBehaviour
    {
        [SerializeField] private BombSpriteByID _bombSprites;
        [SerializeField] private BombMenuUI _ui;
        [Space(5)]
        [SerializeField] private BombUIChannelSO _menuChannel;
        [SerializeField] private BombInventory _inventory;
        private List<string> _shownItems = new List<string>();
        private string _highlighted;

        private void Awake()
        {
            _ui.Init();
            if(_menuChannel != null)
            {
                _menuChannel.ShowMenu = ShowMenu;
                _menuChannel.HideMenu = HideMenu;
                _menuChannel.UpdateView = UpdateView;
                _menuChannel.SetInventory = SetInventory;
                _menuChannel.SetCurrent = HighlightItem;
            }
            else
            {
                Debug.Log("Menu Channel not assingned");
                return;
            }
        }

        public void SetInventory(BombInventory inventory)
        {
            _inventory = inventory;
        }

        public void ShowMenu()
        {
            Debug.Log("show menu");
        }

        public void HideMenu()
        {
            Debug.Log("Hide menu");
        }

        public void UpdateView()
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
                HighlightItem(currentItem);
            }
        }

        public void HighlightItem(string id)
        {
            _ui.Highlight(id);
        }

        
    }
}