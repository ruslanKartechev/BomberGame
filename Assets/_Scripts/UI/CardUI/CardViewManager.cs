using System.Collections;
using UnityEngine;
using TMPro;
using CommonGame;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame.UI
{

    public partial class CardViewManager : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private GeneralDescriptionBlock _generalDescription;
        [SerializeField] private SpecificDescrionBlock _specificDescription;
        [SerializeField] private DurationBlock _durationBlock;
        [SerializeField] private ProperButton _acceptButton;
        [SerializeField] private CardChoiceTimerBlock _timerBlock;
        [Space(10)]
        [SerializeField] private Animator _animator;
        [SerializeField] private GameObject _cardUI;
        [Space(10)]
        [SerializeField] private ActorHighlightManager _bomberHighlighter;
        [Header("Settings")]
        [SerializeField] private CardDisplaySettings _displaySettings;
        private CancellationTokenSource _displayToken;

        private bool _isHidden;

        private void Start()
        {
            _cardUI.SetActive(false);
            _isHidden = true;
        }

        [System.Serializable]
        public struct CardDisplaySettings
        {
            public TimingSettings Timing;
            public float TimeBeforeHide;

        }

        public void SetBlockData(CardDescription data)
        {
            if (_isHidden)
                ShowBlock();
            _title.text = data.Title;
            _generalDescription.SetText(data.GeneralDescription);
            _specificDescription.SetText(data.SpecificDescription);
            _specificDescription.SetValue(data.SetValue);
            _durationBlock.SetTime(data.Duration.ToString() + " s");
            _bomberHighlighter?.Highlight(data.TargetsIDs);
        }

        public async Task<bool> WaitForPlayerChoice(CancellationToken token)
        {
            float elapsed = 0f;
            float time = GetDelay();
            bool pressed = false;
            _timerBlock?.ShowTimer();
            _timerBlock?.RefreshTimer();
            _acceptButton.enabled = true;
            _acceptButton.OnDown += ButtonPressed;
            while (elapsed <= time)
            {
                token.ThrowIfCancellationRequested();
                if(pressed == true)
                {
                    await OnButtonClick();
                    token.ThrowIfCancellationRequested();
                    _acceptButton.OnDown -= ButtonPressed;
                    _bomberHighlighter?.StopHighlightAll();
                    return true;
                }
                _timerBlock?.SetTimer(1 - elapsed / time);
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            token.ThrowIfCancellationRequested();
            _bomberHighlighter?.StopHighlightAll();
            _timerBlock?.HideTimer();
            _acceptButton.enabled = false;
            HideBlock();
            void ButtonPressed()
            {
                pressed = true;
            }
            return false;
        }

        private float GetDelay()
        {
            if (_displaySettings.Timing.Randomize)
            {
                return UnityEngine.Random.Range(_displaySettings.Timing.Delay_min, _displaySettings.Timing.Delay_max);
            }
            else
            {
                return _displaySettings.Timing.DefaultDelay;
            }
        }

        public void ShowBlock()
        {
            _cardUI.SetActive(true);
            _animator.Play("Show");
            _isHidden = false;
        }

        public void HideBlock()
        {
            _animator.Play("Hide");
            _isHidden = true;
        }

        private async Task OnButtonClick()
        {
            _animator.Play("Click");
            await Task.Delay((int)(1000 * _displaySettings.TimeBeforeHide));
            HideBlock();
        }

    }


    
}