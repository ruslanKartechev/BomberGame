using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
namespace BomberGame.UI
{
    public class HealthMenuManager : HealthMenuBase
    {
        [SerializeField] private Image _sprite;
        [SerializeField] private TextMeshProUGUI _text;
        private IDamageEffect _damageEffect;
        private IHealEffect _healEffect;
        private void Awake()
        {
            _damageEffect = GetComponent<IDamageEffect>();
            _healEffect = GetComponent<IHealEffect>();
        }

        public override void SetHealth(string text)
        {
            _text.text = text;
        }

        public override void OnDamage()
        {
            _damageEffect?.ExecuteDamage();
        }

        public override void OnHeal()
        {
            _healEffect?.ExecuteHeal();
        }

    }
}