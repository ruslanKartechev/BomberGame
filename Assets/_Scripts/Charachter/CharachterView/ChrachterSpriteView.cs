using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public class ChrachterSpriteView : CharachterViewBase
    {

        [SerializeField] private bool IsDebug = false;
        [SerializeField] protected InputMoveChannelSO _inputEvents;
        [SerializeField] protected SpriteViewSO _sprites;
        [SerializeField] SpriteRenderer _renderer;
        private void Start()
        {
            if (IsDebug)
            {
                Enable();
            }
        }

        public override void Enable()
        {
            _inputEvents.Up += OnUp;
            _inputEvents.Down += OnDown;
            _inputEvents.Right += OnRight;
            _inputEvents.Left += OnLeft;
        }
        public override void Disable()
        {
            _inputEvents.Up += OnUp;
            _inputEvents.Down += OnDown;
            _inputEvents.Right += OnRight;
            _inputEvents.Left += OnLeft;
        }
        private void OnUp()
        {
            _renderer.sprite = _sprites.Up;
        }
        private void OnDown()
        {
            _renderer.sprite = _sprites.Down;

        }
        private void OnLeft()
        {
            _renderer.sprite = _sprites.Left;

        }
        private void OnRight()
        {
            _renderer.sprite = _sprites.Right;

        }
    }
}