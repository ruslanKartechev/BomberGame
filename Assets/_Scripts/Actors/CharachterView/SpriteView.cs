using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace BomberGame
{
    [System.Serializable]
    public class SpriteView : CharachterViewBase, ITransformView2D, ISpriteView2D
    {
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Transform _transform;
        [SerializeField] private Transform _spritesTransform;
        private bool DoRespond = true;

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

        public void SetViewDirection(char dir)
        {
            try
            {
                RotateView(dir);
            } catch
            {
                Debug.LogError("SpriteViewTransform not assigned");
            }
        }

        private void RotateView(char dir)
        {
            switch (dir)
            {
                case 'u':
                    _spritesTransform.eulerAngles = new Vector3(0, 0, 180);
                    break;
                case 'd':
                    _spritesTransform.eulerAngles = new Vector3(0, 0, 0);
                    break;
                case 'r':
                    _spritesTransform.eulerAngles = new Vector3(0, 0, 90);
                    break;
                case 'l':
                    _spritesTransform.eulerAngles = new Vector3(0, 0, 270);
                    break;
            }
        }


        public Sprite GetCurrentView()
        {
            try
            {
                return _renderer.sprite;

            } catch
            {
                Debug.LogError("Renderer not assigned");
                return null;
            }
        }

        public void SetViewDirection(Vector2 current, Vector2 next)
        {
            if (current.x > next.x)
            {
                RotateView('l');
            }
            else if (current.x < next.x)
            {
                RotateView('r');
            }
            else if (current.y > next.y)
            {
                RotateView('d');
            }
            else if (current.y < next.y)
            {
                RotateView('u');
            }
        }
    }
}