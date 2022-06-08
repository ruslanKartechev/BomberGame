using UnityEngine;
using System.Collections.Generic;
namespace BomberGame
{
    public class RandomPositionGenerator
    {
        public INodeMap<Vector2> _nodeMap;
        public RandomPositionGenerator(INodeMap<Vector2> map)
        {
            _nodeMap = map;
            GetAvailableMapCoords();
        }

        public Vector2 GetPosition(Vector2 currentPosition)
        {
            Vector2 position = currentPosition;
            int i = 0;
            var nodes = _nodeMap.GetNodes();
            List<Vector2> mapPositions = new List<Vector2>(nodes.Keys);
            do
            {
                int index = UnityEngine.Random.Range(0, mapPositions.Count);
                position = mapPositions[index];
                i++;
            } while ((nodes[position].IsWalkable == false || position == currentPosition )&& i <20);
            return position;
        }

        private void GetAvailableMapCoords()
        {
            var nodes = _nodeMap.GetNodes();

        }


        private void GetCoords(List<float> list, float gridSize, float min, float max)
        {
            int count = (int)((max - min) / gridSize );
            float pos = min;
            for(int i = 1; i < count; i++) // 4
            {
                pos = min + gridSize * i;
                list.Add(pos);
            }
        }


    }

}