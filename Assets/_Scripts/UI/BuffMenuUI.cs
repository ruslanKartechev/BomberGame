using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.UI
{


    public class BuffMenuUI : MonoBehaviour
    {
        [SerializeField] private List<BuffUIBlock> _blocks = new List<BuffUIBlock>();
        
        public void Init()
        {

        }

        public void AddBlock(Sprite newSprite, string id)
        {
            BuffUIBlock b = _blocks.Find(t => t.IsActive == false);
            if(b != null)
            {
                b.SetSprite(newSprite);
                b.ID = id;
                b.Show();
            }
            else
            {
                Debug.Log($"Free block not found");
            }
        }

        public void ChangeBlock(Sprite newSprite, string id)
        {
            BuffUIBlock b = _blocks.Find(t => t.ID == id);
            if (b != null)
            {
                b.SetSprite(newSprite);
                b.ID = id;
                b.Show();
            }
            else
            {
                Debug.Log($"Block with id: {id}  not found");
            }
        }

        public void HideAll()
        {
            foreach(BuffUIBlock b in _blocks)
            {
                b.Hide();
            }
        }
    }
}