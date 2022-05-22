using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public class RandomPositionGenerator
    {
        public IObstacleMap _map;
        private float _gridSize;
        private List<float> _mapXcoords;
        private List<float> _mapYcoords;
        private MapBorders _borders;
        private Dictionary<Vector2, IAdaptableObstacle> _obstacles = new Dictionary<Vector2, IAdaptableObstacle>();
        public RandomPositionGenerator(IObstacleMap map)
        {
            _map = map;
            _gridSize = _map.GetGridSize();
            _borders = _map.GetBorders();
            _mapXcoords = new List<float>();
            _mapYcoords = new List<float>();
            GetAvailableMapCoords();
        }
        public Vector2 GetPosition(Vector2 currentPosition)
        {
            CacheObstacles();
            Vector2 position = currentPosition;
            int i = 0;
            var obstacles = _map.GetObstacles();
            do
            {
                int randIndex_x = UnityEngine.Random.Range(0, _mapXcoords.Count);
                int randIndex_y = UnityEngine.Random.Range(0, _mapYcoords.Count);
                float x = _mapXcoords[randIndex_x];
                float y = _mapXcoords[randIndex_y];
                position = new Vector2(x, y);
                i++;
            } while ((CheckPosition(position) == false || position == currentPosition )&& i <20);
            Debug.Log($"position found in {i} iterations");
            return position;
        }

        public bool CheckPosition(Vector2 position)
        {
            if (_obstacles.ContainsKey(position) == true)
            {
                Debug.Log($"obstacle at {position}");
                return false;
            }
            return true;
        }
        private void CacheObstacles()
        {
            _obstacles = _map.GetObstacles();
        }

        private void GetAvailableMapCoords()
        {
            _mapXcoords.Clear();
            GetCoords(_mapXcoords, _gridSize, _borders.Left,_borders.Right);
            GetCoords(_mapYcoords, _gridSize, _borders.Down, _borders.Up);
        }


        private void GetCoords(List<float> list, float gridSize, float min, float max)
        {
            int count = (int)((max - min) / gridSize );
            float pos = min;
            Debug.Log("positions vert count");
            for(int i = 1; i < count; i++) // 4
            {
                pos = min + gridSize * i;
                list.Add(pos);
            }
        }


    }

}