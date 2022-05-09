using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BotSettings", menuName = "SO/BotSettings", order = 1)]

    public class BotSettings : ScriptableObject
    {
        [Header("Movement")]
        public float GridSnapTime = 0.2f;
        [Header("Health")]
        public int StartHealth = 2;
        [Header("Inventories")]
        public BombInventory BombInventory;
        public BuffInventory BuffInventory;
        [Header("Sprites")]
        public SpriteViewSO Sprites;
        [Header("Lists")]
        public BombsPrefabs BombPrefabs;
        public BombBuffs BombBuffers;
        [Header("BombPlacement Time Delay")]
        public float TimeMin;
        public float TimeMax;
    }
}