using UnityEngine;
using TMPro;
namespace BomberGame.UI
{
    public class DurationBlock : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        public void SetTime(string time)
        {
            _text.text = time;
        }
    }


    
}