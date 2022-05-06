using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BomberGame.UI
{
    public class HealthUIManager : MonoBehaviour
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private HealthUIChanngelSO _channel;
        private IDamageEffect _damageEffect;
        private IHealEffect _healEffect;
        private void Awake()
        {
            if(_channel != null)
            {
                _channel.OnDamage = OnDamage;
                _channel.OnHeal = OnHeal;
                _channel.SetHealth = SetHealth;
            }
            _damageEffect = GetComponent<IDamageEffect>();
            _healEffect = GetComponent<IHealEffect>();


        }

        public void SetHealth(string text)
        {
            _text.text = text;
        }

        public void OnDamage()
        {
            _damageEffect?.ExecuteDamage();
        }

        public void OnHeal()
        {
            _healEffect?.ExecuteHeal();
        }

    }
}