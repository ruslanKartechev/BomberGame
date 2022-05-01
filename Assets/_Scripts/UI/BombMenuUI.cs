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
        private List<int> _displayed = new List<int>();
        private Dictionary<int, Vector2> _postions = new Dictionary<int, Vector2>();
        private int appendInd = 0;
        private int shownCount = 0;
        private int highLightInd = 0;
        public void Init()
        {
            for(int i=0; i< _blocks.Count; i++)
            {
                _postions.Add(i,_blocks[i].transform.localPosition);
                _blocks[i].Hide();
            }
            appendInd = 0;
            shownCount = 0;


        }

        public int SetBlock(Sprite sprite, string text)
        {
            //Debug.Log($"append index: {appendInd}");
            BombUIBlock b = _blocks.Find(t => t.IsShown == false);
            if (b == null)
                b = _blocks[_blocks.Count - 1];
            int blockind = _blocks.IndexOf(b);
            _displayed.Insert(0, blockind);

            b.gameObject.SetActive(true);
            b.Show();
            b.SetImage(sprite);
            b.SetText(text);
            //b.SetLocalPosition(_postions[appendInd]);

            //b.OrderPos = appendInd;
            //if (appendInd < _blocks.Count-1)
            //    appendInd++;
            //if(shownCount < _blocks.Count)
            //    shownCount++;
            Reorder();
            return blockind;
        }

        public void HideBlock(int index)
        {
            int order = _blocks[index].OrderPos;
            //if(shownCount < _blocks.Count)
            //    appendInd--;
            //shownCount--;
            _blocks[index].Hide();
            _blocks[index].StopHighlihgt();
            _displayed.Remove(index);
            foreach(BombUIBlock b in _blocks)
            {
                if(b.OrderPos > order)
                {
                    b.OrderPos--;
                }
            }
            _blocks[index].OrderPos = _blocks.Count-1;
            Reorder();
        }

        public void UpdateText(int index, string text)
        {
            _blocks[index].SetText(text);
        }
        
        private void Reorder()
        {
            for(int i=0; i < _displayed.Count; i++)
            {
                _blocks[_displayed[i]].SetLocalPosition(_postions[i]);
            }
        }

        public void Highlight(int index)
        {
            _blocks[highLightInd].StopHighlihgt();
            _blocks[index].Highlight();
            highLightInd = index;
        }

        public void StopHightlight(int index)
        {
            _blocks[index].StopHighlihgt();
        }

    }
}