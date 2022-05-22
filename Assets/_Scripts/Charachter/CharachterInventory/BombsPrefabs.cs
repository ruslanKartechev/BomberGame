using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame {
    [CreateAssetMenu(fileName = "BombsPrefabs", menuName = "SO/BombsPrefabs", order = 1)]
    public class BombsPrefabs : ScriptableObject
    {
        public List<PrefabByID> PrefabByID = new List<PrefabByID>();
        public BombManager GetPrefab(string id)
        {
            BombManager bomb = PrefabByID.Find(t => t.ID == id).Bomb;
            if (bomb == null)
                Debug.Log($"Prefab with id: {id} not found");

            return bomb;
        }
    }
}