using UnityEngine;
using CommonGame.Controlls;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "ControllablePawnSettings", menuName = "SO/Settings/ControllablePawnSettings", order = 1)]

    public class ControllablePawnSettings : PawnSettings
    {
        [Header("Controll Channels")]
        public InputMoveChannelSO InputMoveChannel;
        public InputAttackChannelSO AttackChannel;
    }
}