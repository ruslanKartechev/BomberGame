
using UnityEngine;
using CommonGame.Controlls;
using Zenject;
namespace BomberGame
{
    public abstract class BombPlacerBase : MonoBehaviour
    {
        [Inject] protected InputAttackChannelSO AttackChannel;
        [Inject] protected BombsPrefabs _bombPrefabs;
        public string CharachterID;
        public abstract void Enable();
        public abstract void Disable();
    }
}