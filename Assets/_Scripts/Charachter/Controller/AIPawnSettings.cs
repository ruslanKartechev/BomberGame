using UnityEngine;
namespace BomberGame
{
    [CreateAssetMenu(fileName = "AIPawnSettings", menuName = "SO/Settings/AIPawnSettings", order = 1)]

    public class AIPawnSettings : PawnSettings
    {
        [Space(10)]
        public SpawnSettings _spawnSettings;
    }


    [System.Serializable]
    public struct SpawnSettings
    {
        public Vector2 StartPosition;
    }
}