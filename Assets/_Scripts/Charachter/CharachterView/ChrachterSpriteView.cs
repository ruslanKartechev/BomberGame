using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Controlls;
using Zenject;
namespace BomberGame
{
    public class ChrachterSpriteView : CharachterViewBase
    {
        [SerializeField] private bool IsDebug = false;
        [Inject] protected InputMoveChannelSO _inputEvents;
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
            _renderer.sprite = Sprites.Up;
        }
        private void OnDown()
        {
            _renderer.sprite = Sprites.Down;

        }
        private void OnLeft()
        {
            _renderer.sprite = Sprites.Left;

        }
        private void OnRight()
        {
            _renderer.sprite = Sprites.Right;

        }
    }
}