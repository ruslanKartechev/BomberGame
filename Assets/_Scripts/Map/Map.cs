using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public interface IBombMap<T>
    {
        public event Action<T> OnBombPlaced;
        void PlaceBomb(T position);
        void RemoveBomb(T position);
    }

    [CreateAssetMenu(fileName ="Map", menuName = "SO/Maps/Map_")]
    public class Map : ScriptableObject, IObstacleMap<Vector2>, IActorsMap<Vector2>, INodeMap<Vector2>, IBombMap<Vector2>
    {
        [SerializeField] private float _gridSize = 2;
        [SerializeField] private MapBorders _borders;

        private MapNonMovableTable<Vector2, InteractableEntity> _obstacles = new MapNonMovableTable<Vector2, InteractableEntity>();
        private MapMovingTable<Vector2, InteractableEntity> _actors = new MapMovingTable<Vector2, InteractableEntity>();
        private Dictionary<Vector2, MapNode> _mapNodes = new Dictionary<Vector2, MapNode>();

        public event Action<Vector2> OnBombPlaced;


        #region IObstacleMap
        public List<InteractableEntity> GetObstacles()
        {
            return _obstacles.GetAllEntries();
        }

        void IObstacleMap<Vector2>.AddToMap(Vector2 position, InteractableEntity user)
        {
            _obstacles.AddEntry(user, position);
            _mapNodes.TryGetValue(position, out MapNode val);
            if (val != null)
                val.IsWalkable = false;
        }

        void IObstacleMap<Vector2>.RemoveFromMap(InteractableEntity user)
        {
            _obstacles.RemoveEntry(user);
        }

        public float GetGridSize()
        {
            return _gridSize;
        }
        public InteractableEntity GetObstacleAt(Vector2 position)
        {
           return _obstacles.TryGetAt(position);
        }

        public MapBorders GetBorders()
        {
            return _borders;
        }

        public bool IsOutsideBorders(Vector2 position)
        {
            if (position.x >= _borders.Right)
                return true;
            if (position.x <= _borders.Left)
                return true;
            if (position.y >= _borders.Up)
                return true;
            if (position.y <= _borders.Down)
                return true;
            return false;
        }
        #endregion




        #region IActorsMap
        void IActorsMap<Vector2>.AddToMap(InteractableEntity actor,Vector2 position)
        {
            _actors.AddEntry(actor, position);
        }

        void IActorsMap<Vector2>.RemoveFromMap(InteractableEntity actor)
        {
            _actors.RemoveEntry(actor);
        }

        void IActorsMap<Vector2>.UpdatePosition(InteractableEntity actor, Vector2 position)
        {
            _actors.UpdateEntryPosition(actor, position);
        }

        List<InteractableEntity> IActorsMap<Vector2>.GetAllActors()
        {
            return _actors.GetAllEntries();
        }
        Dictionary<InteractableEntity, Vector2> IActorsMap<Vector2>.GetAllActorsPositions()
        {
            return _actors.PositionTable;
        }

        #endregion



        #region NodeMap
        public void GenerateMap()
        {
            List<Vector2> grid = GenerateGrid();
            _mapNodes = GenerateMapNodes(grid);
        }
        private List<Vector2> GenerateGrid()
        {
            int cell_count_hor = (int)(Mathf.Abs(_borders.Right - _borders.Left) / _gridSize);
            int cell_count_vert = (int)(Mathf.Abs(_borders.Up - _borders.Down) / _gridSize);
            float[] x_vals = new float[cell_count_hor-1];
            float[] y_vals = new float[cell_count_hor-1];
            List<Vector2> grid = new List<Vector2>(cell_count_hor + cell_count_hor - 2 );
            for (int i = 1; i < cell_count_hor; i++)
            {
                x_vals[i-1] = _borders.Left + _gridSize * i;
            }
            for (int i = 1; i < cell_count_vert; i++)
            {
                y_vals[i-1] = _borders.Down + _gridSize * i;
            }
            for(int i=0; i < x_vals.Length; i++)
            {
                for(int j=0; j < y_vals.Length; j++)
                {
                    Vector2 v = new Vector2(x_vals[i], y_vals[j]);
                    grid.Add(v);
                }
            }
            return grid;
        }

        private Dictionary<Vector2, MapNode> GenerateMapNodes(List<Vector2> grid)
        {
            Dictionary<Vector2, MapNode> nodes = new Dictionary<Vector2, MapNode>(grid.Count);
            foreach(Vector2 v in grid)
            {
                MapNode node = new MapNode(v, this);
                nodes.Add(v,node);
            }
            return nodes;
        }
        
        public Node<Vector2> GetNodeAt(Vector2 position)
        {
            return _mapNodes[position];
        }
        public Dictionary<Vector2, MapNode> GetNodes()
        {
            return _mapNodes;
        }
        #region MapNodeNeighbours
        public List<Node<Vector2>> GetNeighbours(Node<Vector2> node)
        {
            List <Node<Vector2>> others = new List<Node<Vector2>>();
            Node<Vector2> l = GetLeftCell(node.Value);
            Node<Vector2> r = GetRightCell(node.Value);
            Node<Vector2> u = GetUpCell(node.Value);
            Node<Vector2> d = GetDownCell(node.Value);
            if (l != null)
                others.Add(l);
            if (r != null)
                others.Add(r);
            if (u != null)
                others.Add(u); 
           if (d != null)
                others.Add(d);
            return others;
        }

        private Node<Vector2> GetLeftCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.left * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetRightCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.right * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetUpCell(Vector2 gridPosition)
        {
            gridPosition += Vector2.up * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> GetDownCell(Vector2 gridPosition)
        {
            gridPosition -= Vector2.up * _gridSize;
            return TryGetCell(gridPosition);
        }

        private Node<Vector2> TryGetCell(Vector2 gridPosition)
        {
            MapNode res = null;
            if (_mapNodes.TryGetValue(gridPosition, out res))
            {
                if (res.IsWalkable == true)
                    return res;
            }
            return null;
        }



        #endregion
        #endregion



        public void PlaceBomb(Vector2 position)
        {
            OnBombPlaced?.Invoke(position);
        }

        public void RemoveBomb(Vector2 position)
        {
            
        }


    }


}