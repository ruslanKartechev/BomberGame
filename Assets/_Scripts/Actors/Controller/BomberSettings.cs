using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CommonGame.Events;
using BomberGame.UI;
using BomberGame.Health;
using BomberGame.Bombs;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "BomberSettings", menuName = "SO/Actors/BomberSettings", order = 1)]

    public class BomberSettings : ScriptableObject
    {
        [Header("Movement")]
        public MoveSettings MovementSettings;
        [Header("Health")]
        public HealthSettings  Health;
        [Header("Inventories")]
        public BombInventory BombInventory;
        [Header("Bombs")]
        public BombsPrefabContainer BombPrefabs;
        [Header("bomb channel")]
        public BombPoolChannelSO _bombChannel;
    }
}