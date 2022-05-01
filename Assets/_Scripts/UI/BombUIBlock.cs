using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BomberGame.UI
{
    public class BombUIBlock : MonoBehaviour
    {
        public Image BombImage;
        public TextMeshProUGUI Text;
        public Image HighlightImage;
        [HideInInspector] public int OrderPos;
        [HideInInspector] public bool IsShown = false;
        private bool isHighlighted = false;

        public void SetText(string text)
        {
            Text.text = text;
        }

        public void Show()
        {
            BombImage.gameObject.SetActive(true);
            Text.gameObject.SetActive(true);
            IsShown = true;
        }

        public void Hide()
        {
            BombImage.gameObject.SetActive(false);
            Text.gameObject.SetActive(false);
            HighlightImage.gameObject.SetActive(false);
            IsShown = false;
        }

        public void SetImage(Sprite sprite)
        {
            BombImage.sprite = sprite;
        }
       
        public void SetLocalPosition(Vector3 position)
        {
            transform.localPosition = position;
        }

        public void Highlight()
        {
            if(isHighlighted == false)
            {
                HighlightImage.gameObject.SetActive(true);
                isHighlighted = true;
            }
        }

        public void StopHighlihgt()
        {
            if(isHighlighted == true)
            {
                HighlightImage.gameObject.SetActive(false);
                isHighlighted = false;
            }
        }
    }
}