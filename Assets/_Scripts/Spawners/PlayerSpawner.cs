using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public class PlayerSpawner : MonoBehaviour
    {
        [SerializeField] private CharachterComponents _playerPrefab;
        [SerializeField] private Transform _defaultSpawn;
        public void SpawnPlayer()
        {
            CharachterComponents components = Instantiate(_playerPrefab);
            if (_defaultSpawn != null)
                components.gameObject.transform.position = _defaultSpawn.position;
            else
                components.gameObject.transform.position = Vector3.zero;
        }
    }
}