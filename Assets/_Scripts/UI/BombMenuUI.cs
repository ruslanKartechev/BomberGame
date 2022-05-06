using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BomberGame.UI
{

    public class BombMenuUI : MonoBehaviour
    {
        [SerializeField] private List<BombUIBlock> _blocks = new List<BombUIBlock>();
        private List<string> _displayed = new List<string>();
        private Dictionary<int, Vector2> _postions = new Dictionary<int, Vector2>();
        private BombUIBlock _active;
        public void Init()
        {
            for(int i=0; i< _blocks.Count; i++)
            {
                _postions.Add(i,_blocks[i].transform.localPosition);
                _blocks[i].Hide();
            }
        }

        public void SetBlock(Sprite sprite, string text, string id)
        {
            BombUIBlock b = _blocks.Find(t => t.IsShown == false);
            if (b == null)
                b = _blocks[_blocks.Count - 1];

            b.ID = id;
            _displayed.Insert(0, id);

            b.gameObject.SetActive(true);
            b.Show();
            b.SetImage(sprite);
            b.SetText(text);
            Reorder();
        }

        public void HideBlock(string id)
        {
            BombUIBlock b = _blocks.Find(t => t.ID == id);
            int order = b.OrderPos;
            b.Hide();
            b.StopHighlihgt();
            _displayed.Remove(id);
            foreach(BombUIBlock block in _blocks)
            {
                if(block.OrderPos > order)
                {
                    block.OrderPos--;
                }
            }
            b.OrderPos = _blocks.Count-1;
            Reorder();
        }

        public void UpdateText(string id, string text)
        {
            if(_active.ID == id)
            {
                _active.SetText(text);
            }
            else
            {
                BombUIBlock b = _blocks.Find(t => t.ID == id);
                if (b)
                {
                    b.SetText(text);
                }
            }
        }
        
        private void Reorder()
        {
            for(int i=0; i < _displayed.Count; i++)
            {
               // _blocks[_displayed[i]].SetLocalPosition(_postions[i]);
            }
        }

        public void Highlight(string id)
        {
            StopHightlight();
            BombUIBlock b = _blocks.Find(t => t.ID == id);
            if (b)
            {
                _active = b;
                b.Highlight();
            }
        }

        public void StopHightlight()
        {
            if (_active)
            {
                _active.StopHighlihgt();
            }
        }

    }
}