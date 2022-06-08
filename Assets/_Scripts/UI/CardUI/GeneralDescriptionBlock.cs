using UnityEngine;
using TMPro;
namespace BomberGame.UI
{
    public class GeneralDescriptionBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void SetText(string text)
        {
            _text.text = text;
        }
    }


    
}