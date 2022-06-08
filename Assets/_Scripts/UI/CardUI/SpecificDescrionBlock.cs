using UnityEngine;
using TMPro;
namespace BomberGame.UI
{
    public class SpecificDescrionBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _mainText;
        [SerializeField] private TextMeshProUGUI _valueText;

        public void SetText(string text)
        {
            _mainText.text = text;
        }
        public void SetValue(string text)
        {
            _valueText.text = text;
        }
    }


    
}