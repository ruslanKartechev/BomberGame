using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using CommonGame;
namespace BomberGame.Bombs
{


    [CreateAssetMenu(fileName = "BombPoolChannel", menuName = "SO/Bombs/BombPoolChannel")]
    public class BombPoolChannelSO : ScriptableObject
    {
        [SerializeField] private string _defaultBombID;
        [SerializeField] private BombsPrefabContainer _prefabs;
        [HideInInspector] public DefaultBombPoolSpawner _defPoolSpawner;

        private Dictionary<string, IBombPoolGetter> _poolGetters = new Dictionary<string, IBombPoolGetter>();
        public void InitDefaultBombPool()
        {
            if(_defPoolSpawner != null)
            {
                Bomb defaultBombPrefab = _prefabs.GetPrefab(_defaultBombID);
                _defPoolSpawner.CreateFromPrefab(defaultBombPrefab.gameObject);
                _poolGetters.Add(_defaultBombID, _defPoolSpawner);
            }
            else
            {
                throw new Exception("DefaultPiilSpawner not assigned. Cannot spawn object pool for default Bomb");
            }
        }

        public Bomb GetBombByID(string id)
        {
            if (_poolGetters.ContainsKey(id))
            {
                try
                {
                    Bomb b = _poolGetters[id].GetBombFromPool();
                    return b;
                }
                catch
                {
                    Debug.Log($"Pool did not return any bomb ID: {id}");
                    return null;
                }
            }
            else
            {
                throw new Exception($"Did not create a pool for ID:  {id}");
            }
        }




    }
}