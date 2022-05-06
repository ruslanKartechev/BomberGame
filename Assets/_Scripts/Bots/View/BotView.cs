using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class BotView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _rend;
        private EnemyMover _mover;
        public SpriteViewSO Sprites;

        private void Awake()
        {
            if (_rend == null)
                _rend = GetComponent<SpriteRenderer>();
        }
        public void Init(EnemyMover mover, SpriteViewSO sprites)
        {
            _mover = mover;
            Sprites = sprites;
            _mover.DirectionChange += OnDirectionChange;
        }


        private void OnDirectionChange()
        {
            Vector3 dir = _mover.CurrentDir;
            if(dir.x == 0)
            {
                if(dir.y >= 0)
                {
                    _rend.sprite = Sprites.Up;
                } else
                {
                    _rend.sprite = Sprites.Down;

                }
            } else if(dir.y == 0)
            {
                if (dir.x >= 0)
                {
                    _rend.sprite = Sprites.Right;

                }
                else
                {
                    _rend.sprite = Sprites.Left;

                }
            }
        }



    }
}