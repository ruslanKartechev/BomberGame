
using UnityEngine;

namespace BomberGame
{
    public class Wall : MonoBehaviour, IWall
    {
        [SerializeField] private WallType _type;

        WallType IWall.GetType()
        {
            return _type;
        }
    }
}