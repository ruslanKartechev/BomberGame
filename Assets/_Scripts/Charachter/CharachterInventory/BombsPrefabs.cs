using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame {
    [CreateAssetMenu(fileName = "BombsPrefabs", menuName = "SO/BombsPrefabs", order = 1)]
    public class BombsPrefabs : ScriptableObject
    {
        public List<PrefabByID> PrefabByID = new List<PrefabByID>();
        public GameObject GetPrefab(string id)
        {
            GameObject pf = PrefabByID.Find(t => t.ID == id).PF;
            if (pf == null)
                Debug.Log($"Prefab with id: {id} not found");

            return pf;
        }
    }
}