using UnityEngine;
using CommonGame.Controlls;
using BomberGame.Bombs;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "PlayerBomberSettings", menuName = "SO/Actors/PlayerBomberSettings", order = 1)]

    public class PlayerBomberSettings : ScriptableObject
    {
        [Header("Controll Channels")]
        public InputMoveChannelSO InputMoveChannel;
        public InputAttackChannelSO AttackChannel;

    }
}