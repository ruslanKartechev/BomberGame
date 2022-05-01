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
        private Dictionary<string, int> _itemsCount = new Dictionary<string, int>();
        private Dictionary<string, int> _elementID = new Dictionary<string, int>();

        private void Awake()
        {
            _ui.Init();
            if(_menuChannel != null)
            {
                _menuChannel.ShowMenu = ShowMenu;
                _menuChannel.HideMenu = HideMenu;
                _menuChannel.AddItem = AddItem;
                _menuChannel.RemoveItem = RemoveItem;
                _menuChannel.UpdateCount = UpdateCount;
                _menuChannel.SetCurrentItem = SetCurrentItem;
            }
            else
            {
                Debug.Log("menu channel not assingned");
                return;
            }

        }

        public void ShowMenu()
        {
            Debug.Log("show menu");
        }

        public void HideMenu()
        {
            Debug.Log("Hide menu");
        }


        public void AddItem(string id, int count)
        {
            if(_itemsCount.ContainsKey(id) == false)
            {
                _itemsCount.Add(id, count);
                Sprite sprite = _bombSprites.GetSprite(id);
                if (sprite == null)
                {
                    Debug.Log($"sprite {id} was not found");
                    return;
                }
                int bInd = _ui.SetBlock(sprite, count.ToString());
                _elementID.Add(id, bInd);
                _ui.Highlight(bInd);
            }
            else
            {
                UpdateCount(id, count);
            }
        }

        public void UpdateCount(string id, int count)
        {
            if (_itemsCount.ContainsKey(id) == true)
            {
                _ui.UpdateText(_elementID[id], count.ToString());
            }
            else
            {
                AddItem(id, count);
            }
        }

        public void SetCurrentItem(string id)
        {
            if (_elementID.ContainsKey(id) == true)
            {
                _ui.Highlight(_elementID[id]);
            }
            else
            {
                Debug.Log($"Element {id} not added. Cannot highlight");
            }
        }

        public void RemoveItem(string id)
        {
            if(_itemsCount.ContainsKey(id) == false)
            {
                Debug.Log($"item {id} was not stored");
                return;
            }
     
            _ui.HideBlock(_elementID[id]);
            _itemsCount.Remove(id);
            _elementID.Remove(id);
        }
        
    }
}