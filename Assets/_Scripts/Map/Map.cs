using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{

    public class NodeMapCopy : INodeMap<Vector2>
    {

        private Dictionary<Vector2, MapNode> _nodes = new Dictionary<Vector2, MapNode>();
        private float _gridSize;
        private MapNodeNeighbours4x _neighboursAlg;
        private INodeMap<Vector2> _original;
        public NodeMapCopy(INodeMap<Vector2> original)
        {
            _gridSize = original.GetGridSize();
            _neighboursAlg = new MapNodeNeighbours4x();
            _original = original;
            _nodes = CopyMap(_original.GetNodes());

        }

        public void SetUnwalkable(List<Vector2> positions)
        {
            if (positions == null)
                return;
            Debug.Log($"Set {positions.Count} unwalkable");
            foreach(Vector2 v in positions)
            {
                if(_nodes.TryGetValue(v, out MapNode node))
                {
                    node.IsWalkable = false;
                }
            }
        }

        public void SyncToMap(INodeMap<Vector2> map)
        {
            _nodes = CopyMap(_original.GetNodes());
            _gridSize = map.GetGridSize();
        }

        public void SyncToOriginal()
        {
            _nodes = CopyMap(_original.GetNodes());
        }

        public float GetGridSize() => _gridSize;

        public Node<Vector2> GetNodeAt(Vector2 position)
        {
            _nodes.TryGetValue(position, out MapNode node);
            return node;
        }

        public Dictionary<Vector2, MapNode> GetNodes()
        {
            return _nodes;
        }

        public List<Node<Vector2>> GetNeighbours(Node<Vector2> node)
        {
            return _neighboursAlg.GetNeighbours(node.Value, _nodes, _gridSize);
        }

        private Dictionary<Vector2, MapNode> CopyMap(Dictionary<Vector2, MapNode> from)
        {
            Dictionary<Vector2, MapNode> @out = new Dictionary<Vector2, MapNode>();
            foreach (Vector2 v in from.Keys)
            {
                MapNode node = new MapNode(v, this);
                node.IsWalkable = from[v].IsWalkable;
                @out.Add(v, node);
            }

            return @out;
        }
      
    }


    [CreateAssetMenu(fileName ="Map", menuName = "SO/Maps/Map_")]
    public class Map : ScriptableObject, IObstacleMap<Vector2>, IActorsMap<Vector2>, INodeMap<Vector2>, IBombMap<Vector2>
    {
        public event Action<Vector2> OnBombPlaced;

        [SerializeField] private float _gridSize = 2;
        [SerializeField] private MapBorders _borders;

        private MapNonMovableTable<Vector2, InteractableEntity> _obstacles = new MapNonMovableTable<Vector2, InteractableEntity>();
        private MapMovingTable<Vector2, InteractableEntity> _actors = new MapMovingTable<Vector2, InteractableEntity>();
        private Dictionary<Vector2, MapNode> _nodes = new Dictionary<Vector2, MapNode>();
        private MapNodeNeighboursAlgorithm<Vector2> _neighboursAlg;


        #region IObstacleMap
        public List<InteractableEntity> GetObstacles()
        {
            return _obstacles.GetAllEntries();
        }

        void IObstacleMap<Vector2>.AddToMap(Vector2 position, InteractableEntity user)
        {
            _obstacles.AddEntry(user, position);
            _nodes.TryGetValue(position, out MapNode val);
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

        public Vector2 GetActorNodePosition(InteractableEntity user)
        {
            var table = _actors.PositionTable;
            if(table.TryGetValue(user, out Vector2 realPos))
            {
                return GetClosestNodePos(realPos);
            }
            throw new System.Exception($"Entity {user.EntityID} was not found on the map");
        }

        private Vector2 GetClosestNodePos(Vector2 realPos)
        {
            Vector2 result = new Vector2();
            result.x = Mathf.Round(realPos.x / _gridSize) * _gridSize;
            result.y = Mathf.Round(realPos.y / _gridSize) * _gridSize;

            return result;
        }
        #endregion



        #region NodeMap
        public void GenerateMap()
        {
            _neighboursAlg = new MapNodeNeighbours4x();
            List<Vector2> grid = GenerateGrid();
            _nodes = GenerateMapNodes(grid);
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
            return _nodes[position];
        }

        public Dictionary<Vector2, MapNode> GetNodes()
        {
            return _nodes;
        }

        public List<Node<Vector2>> GetNeighbours(Node<Vector2> node)
        {
            return _neighboursAlg.GetNeighbours(node.Value, _nodes, _gridSize);
        }
        #endregion



        #region IBombMap
        public void PlaceBomb(Vector2 position)
        {
            OnBombPlaced?.Invoke(position);
        }

        public void RemoveBomb(Vector2 position)
        {
            
        }
        #endregion

    }


}