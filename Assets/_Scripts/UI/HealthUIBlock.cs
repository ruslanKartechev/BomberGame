
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame.UI
{
    public class HealthUIBlock : UIBlock
    {
        public bool Debug = false;
        public float _TextSetTime = 0.25f;
        [SerializeField] private Image _sprite;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private HealthUIChanngelSO _channel;
        [SerializeField] private HealthUIEffects _effects;
        private CancellationTokenSource _token;
        private int _currentValue = 0;

        private void Start()
        {
            if(Debug == true && _channel != null)
            {
                Init(_channel);
            }
        }

        public void Init(HealthUIChanngelSO channel)
        {
            _channel = channel;
            _channel.OnDamage = OnDamage;
            _channel.OnHeal = OnHeal;
            _channel.Update = UpdateValue;
        }

        public void UpdateValue(int val)
        {
            _text.text = val.ToString();
            _token?.Cancel();
            _token = new CancellationTokenSource();
            ValueChange(_currentValue, val, _TextSetTime, _text,_token);
            _currentValue = val;
        }

        public void OnDamage()
        {
            
        }

        public void OnHeal()
        {
           
        }

        public override void Show()
        {

        }

        public override void Hide()
        {
           
        }

        public async void ValueChange(float from, float to, float time, TextMeshProUGUI text,CancellationTokenSource token)
        {
            float elapsed = 0f;
            float value = from;
            while(elapsed <= time && token.IsCancellationRequested == false)
            {
                value = (int)Mathf.Lerp(from,to, elapsed/time);
                text.text = value.ToString();
                elapsed += Time.deltaTime;
                await Task.Yield();
            }
            if(token.IsCancellationRequested == false)
            {
                value = to;
                text.text = value.ToString();
            }

        }



    }
}