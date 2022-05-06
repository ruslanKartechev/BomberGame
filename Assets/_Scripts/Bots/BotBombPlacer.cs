using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace BomberGame
{
    public class BotBombPlacer : MonoBehaviour
    {
        public BombsPrefabs BombPrefabs;
        [SerializeField] private EnemyMover _mover;
        private float _timeMin;
        private float _timeMax;
        private Coroutine _bombCountDown;
        private CharachterInventory _inventory;
        public void Init(EnemyMover mover, BombsPrefabs bombs, float timeMin, float timeMax)
        {
            _mover = mover;
            BombPrefabs = bombs;
            _timeMax = timeMax;
            _timeMin = timeMin;
            _inventory = GetComponent<CharachterInventory>();
            if (_inventory == null)
            {
                Debug.Log("Inventory not found");
            }
        }

        public void Enable()
        {
            if (_bombCountDown != null)
                StopCoroutine(_bombCountDown);
            _bombCountDown = StartCoroutine(PlaceCountdown());
        }

        public void Disable()
        {
            if (_bombCountDown != null)
                StopCoroutine(_bombCountDown);
        }


        private float _placeDelay = 1;
        private IEnumerator PlaceCountdown()
        {
            float elapsed = 0f;
            _placeDelay = GetTimeDelay();
            while (true)
            {
                elapsed += Time.deltaTime;
                if(elapsed >= _placeDelay)
                {
                    PlaceBomb();
                    _placeDelay = GetTimeDelay();
                    elapsed = 0f;
                }

                yield return null;
            }
        }

        private void PlaceBomb()
        {
            if (_inventory == null)
                return;
            string id = _inventory.GetBomb();
            if (id == "empty")
                return;
            GameObject b = BombPrefabs.GetPrefab(id);
            b = Instantiate(b);
            BombBase bomb = b.GetComponent<BombBase>();
            Vector3 position = _mover.PrevPosition;
            bomb.Place(position);
            bomb.InitCoundown();
            // _buffer?.BuffBomb(b);
            //if (_bombInventory.RemoveItem(id, 1))
            //{

            //}
        }

        private float GetTimeDelay()
        {
            return UnityEngine.Random.Range(_timeMin, _timeMax);
        }

    }
}