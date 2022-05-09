using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Events;
using CommonGame.Controlls;
using BomberGame.UI;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "PlayerSettingsSO", menuName = "SO/Settings/PlayerSettingsSO", order = 1)]

    public class PlayerSettingsSO : ScriptableObject
    {

        [Header("Movement")]
        public float GridSnapTime = 0.2f;
        [Header("Health")]
        public int StartHealth = 2;
        [Header("Inventories")]
        public BombInventory BombInventory;
        public BuffInventory BuffInventory;

    }
}