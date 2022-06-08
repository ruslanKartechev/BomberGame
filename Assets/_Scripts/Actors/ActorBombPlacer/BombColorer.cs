
using UnityEngine;

namespace BomberGame
{
    [System.Serializable]
    public class BombColorer
    {
        [SerializeField] private Color32 _bombColor;
        public void ColorBomb(GameObject bomb)
        {
            BombColorBase bombColor = bomb.GetComponent<BombColorBase>();
            if (bombColor != null)
                bombColor.SetColor(_bombColor);
        }
    }
}