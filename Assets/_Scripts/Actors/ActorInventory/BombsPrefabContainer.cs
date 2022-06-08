using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame.Bombs
{

    [CreateAssetMenu(fileName = "BombsPrefabContainer", menuName = "SO/Containers/BombsPrefabContainer", order = 1)]
    public class BombsPrefabContainer : ScriptableObject
    {
        public List<PrefabByID> PrefabByID = new List<PrefabByID>();

        public Bomb GetPrefab(string id)
        {
            Bomb bomb = PrefabByID.Find(t => t.ID == id)._Bomb;
            if (bomb == null)
                throw new System.Exception($"Prefab with id: {id} not found");
            return bomb;
        }

    }

}