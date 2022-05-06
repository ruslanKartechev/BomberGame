using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace BomberGame.UI
{
    public class BuffUIBlock : MonoBehaviour
    {
        [HideInInspector] public string ID;
        [HideInInspector] public bool IsActive = false;

        [SerializeField] private Image _image;
        [SerializeField] private Animator _anim;

        private void Awake()
        {
            
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public void Show()
        {
            _anim.Play("Show");
            _image.enabled = true;
            IsActive = true;
        }
        
        
        public void Hide()
        {
            _image.enabled = false;
            IsActive = false;

        }
    }
}