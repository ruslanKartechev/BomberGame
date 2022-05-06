using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public interface IDroppable
    {
        void Drop();
    }
    public class DroppableBuff : BuffProvider, IDroppable
    {
        [SerializeField] private bool StartDropped;
        [Space(10)]
        [SerializeField] protected Animator _anim;
        [SerializeField] protected SpriteRenderer _rend;
        [SerializeField] protected BuffSpriteByID _sprites;
        [Header("Hidden")]
        [SerializeField] protected Color HiddenColor;
        [SerializeField] protected string HiddenAnimName;

        [Header("Dropped")]
        [SerializeField] protected Color DroppedColor;
        [SerializeField] protected string DroppedAnimName;
        private void Start()
        {
            _rend.color = HiddenColor;
            _anim.Play(HiddenAnimName);
            _collider.enabled = false;
            if (StartDropped)
                Drop();
        }
        public void Drop()
        {
            _collider.enabled = true;
            _rend.sprite = _sprites.BuffSprites.Find(t => t.ID == _myBuff.ID).Sprite;
            _rend.color = DroppedColor;
            _anim.Play(DroppedAnimName);
        }
    }
}