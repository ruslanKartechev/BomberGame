using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    [System.Serializable]
    public class SpriteView : CharachterViewBase, ITransformView2D, ISpriteView
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Transform _transform;
        private bool DoRespond = true;
        private SpriteViewSO _sprites;

        public void Init(SpriteViewSO sprites)
        {
            _sprites = sprites;
        }
        public override void Enable()
        {
            DoRespond = true;
        }
        public override void Disable()
        {
            DoRespond = false;
        }

        public Vector2 GetPostion()
        {
            return _transform.position;
        }

        public void UpdatePosition(Vector2 newPos)
        {
            if (DoRespond)
                _transform.position = new Vector3(newPos.x, newPos.y, _transform.position.z);
        }

        public void SetView(char dir)
        {
            if (_renderer == null || _sprites == null)
            {
                Debug.LogError("Something is missing");
                return;
            }
            switch (dir)
            {
                case 'u':
                    _renderer.sprite = _sprites.Up;
                    break;
                case 'd':
                    _renderer.sprite = _sprites.Down;
                    break;
                case 'r':
                    _renderer.sprite = _sprites.Right;
                    break;
                case 'l':
                    _renderer.sprite = _sprites.Left;
                    break;
            }

        }
    }
}