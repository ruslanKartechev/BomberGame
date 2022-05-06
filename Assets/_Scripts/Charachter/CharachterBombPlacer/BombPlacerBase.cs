
using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    public abstract class BombPlacerBase : MonoBehaviour
    {
        public InputAttackChannelSO AttackChannel;
        public BombsPrefabs _bombPrefabs;
        public abstract void Enable();
        public abstract void Disable();
    }
}