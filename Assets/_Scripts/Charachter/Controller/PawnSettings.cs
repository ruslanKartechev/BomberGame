using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Events;
using BomberGame.UI;
using BomberGame.Health;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "PawnSettings", menuName = "SO/Settings/PawnSettings", order = 1)]

    public class PawnSettings : ScriptableObject
    {
        [Header("Movement")]
        public MoveSettings MovementSettings;
        [Header("View")]
        public SpriteViewSO Sprites;
        [Header("Health")]
        public HealthSettings  Health;
        [Header("Inventories")]
        public BombInventory BombInventory;
        public BuffInventory BuffInventory;
        [Header("Bombs")]
        public BombsPrefabs BombPrefabs;

        [Header("Channels")]
        public HealthUIChanngelSO _healthChannel;

    }
}