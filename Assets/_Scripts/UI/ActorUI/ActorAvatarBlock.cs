using UnityEngine;
using UnityEngine.UI;
namespace BomberGame.UI
{
    public class ActorAvatarBlock : MonoBehaviour
    {
        [SerializeField] private Image _avatarImage;
        private ISpriteView2D _spriteView;
        public void Init(ISpriteView2D spriteView)
        {
            _spriteView = spriteView;
            _avatarImage.sprite = _spriteView?.GetCurrentView();
        }
    }

}