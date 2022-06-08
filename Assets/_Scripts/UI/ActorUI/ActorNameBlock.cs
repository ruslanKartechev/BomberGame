using UnityEngine;
using TMPro;
namespace BomberGame.UI
{
    public class ActorNameBlock : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI _nameText;
        public void Init(string name)
        {
            _nameText.text = name;
        }
    }

}