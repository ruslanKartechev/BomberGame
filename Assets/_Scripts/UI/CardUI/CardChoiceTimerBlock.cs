using UnityEngine;
using UnityEngine.UI;
namespace BomberGame.UI
{
    public class CardChoiceTimerBlock : MonoBehaviour
    {
        [SerializeField] private Image _barImage;
        [SerializeField] private Animator _anim;
        private float _currentTimer = 1f;
        public void ShowTimer()
        {
            gameObject.SetActive(true);
        }
        public void HideTimer()
        {
            gameObject.SetActive(false);

        }
        public void RefreshTimer()
        {
            _currentTimer = 1f;
            _barImage.fillAmount = 1f;
        }
        public void SetTimer(float value)
        {
            _barImage.fillAmount = value;
        }
    }


    
}