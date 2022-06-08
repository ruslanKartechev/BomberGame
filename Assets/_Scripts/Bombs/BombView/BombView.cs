using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class BombView : MonoBehaviour, IBombView
    {
        [SerializeField] private SpriteRenderer _mainBombRenderer;


        public void Show()
        {
            _mainBombRenderer.enabled = true;
        }


        public void Hide()
        {
            _mainBombRenderer.enabled = false;
        }


    }
}