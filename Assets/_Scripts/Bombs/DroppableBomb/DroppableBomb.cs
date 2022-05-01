
using UnityEngine;


namespace BomberGame
{
    public class DroppableBomb : MonoBehaviour, IDroppable, IStorable
    {

        [SerializeField] protected string _dropID;
        [SerializeField] protected int _dropCount;
        [SerializeField] protected BombSpriteByID _sprites;
        [Space(5)]
        [SerializeField] Collider2D _collider;
        [SerializeField] protected Animator _anim;
        [SerializeField] protected SpriteRenderer _rend;
        [Space(5)]
        [Header("Hidden")]
        [SerializeField] protected Sprite _hiddenSprite;
        [SerializeField] protected Vector3 _hiddenScale;
        [SerializeField] protected Color _hiddenColor;
        [SerializeField] protected string _hiddenAnimName;

        [Header("Dropped")]
        [SerializeField] protected Vector3 _droppedScale;
        [SerializeField] protected Color _droppedColor;
        [SerializeField] protected string _droppedAnimName;
        private void Start()
        {
            SetHiddenView();
            _anim.Play(_hiddenAnimName);
            _collider.enabled = false;
        }
        public void Drop()
        {
            _collider.enabled = true;
            SetDroppedView();
            _anim.Play(_droppedAnimName);
        }
        public void SetDroppedView()
        {
            _rend.color = _droppedColor;
            _rend.sprite = _sprites.SpriteByID.Find(t => t.ID == _dropID).Sprite;
            transform.localScale = _droppedScale;
        }
        public void SetHiddenView()
        {
            transform.localScale = _hiddenScale;
            _rend.color = _hiddenColor;
            _rend.sprite = _hiddenSprite;
        }

        public void Store(IInventory inventory)
        {
            inventory.AddItem(_dropID, _dropCount);
            Destroy(gameObject);
        }

        public string GetID()
        {
            return _dropID;
        }
    }
}
