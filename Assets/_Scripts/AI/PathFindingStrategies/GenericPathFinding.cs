using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BomberGame
{
    public enum PathFinderStatus
    {
        NOT_INIT, SUCCESS, FAILURE, RUNNING
    }


    public abstract class PathFinder<T>
    {
        public class PathFinderNode
        {
            public PathFinderNode Parent;
            public Node<T> Location;
            public float Fcost { get; private set; } // full
            public float Hcost { get; private set; } // heuristic
            public float Gcost { get; private set; } // from start to this
            public PathFinderNode(Node<T> node, PathFinderNode parent, float G, float H)
            {
                Location = node;
                Parent = parent;
                Hcost = H;
                SetGcost(G);
            }
            public void SetGcost(float G) // can be updated
            {
                Gcost = G;
                Fcost = Fcost + Hcost;
            }
        }


        public PathFinderStatus Status { get; private set; }
        public delegate float HeuristicCostFunc(T a, T b);
        public delegate float NodeTravelCostFunc(T a, T b);

        public HeuristicCostFunc HeuristicCost;
        public NodeTravelCostFunc NodeTravelCost;

        #region List
        protected List<PathFinderNode> _openList = new List<PathFinderNode>();
        protected List<PathFinderNode> _closedList = new List<PathFinderNode>();

        private PathFinderNode GetLeastCostNode(List<PathFinderNode> fromList)
        {
            int best_index = 0;
            float best_cost = fromList[0].Fcost;
            for (int i = 0; i < fromList.Count; i++)
            {
                if (best_cost > fromList[i].Fcost)
                {
                    best_cost = fromList[i].Fcost;
                    best_index = i;
                }
            }
            return fromList[best_index];
        }

        protected int CheckInList(List<PathFinderNode> fromList, T item)
        {
            for (int i = 0; i < fromList.Count; i++)
            {
                if (EqualityComparer<T>.Default.Equals(fromList[i].Location.Value, item))
                {
                    return i;
                }
            }
            return -1;
        }
        #endregion

        #region Delegates
        public delegate void PathFinderProcessDelegate(PathFinderNode node);
        public PathFinderProcessDelegate onChangeCurrentNode;
        public PathFinderProcessDelegate onAddToOpenList;
        public PathFinderProcessDelegate onAddToClosedList;
        public PathFinderProcessDelegate onDestinationFound;
        #endregion

        public Node<T> Start { get; private set; }
        public Node<T> Goal { get; private set; }
        protected PathFinderNode _currentNode = null;
        public PathFinderNode CurrentNode
        {
            get { return _currentNode; }
        }

        public void Reset()
        {
            if (Status == PathFinderStatus.RUNNING)
            {
                return;
            }
            _openList.Clear();
            _closedList.Clear();
            Status = PathFinderStatus.NOT_INIT;
        }

        public bool Initialize(Node<T> start, Node<T> goal)
        {
            if (Status == PathFinderStatus.RUNNING)
            {
                // we cannot initialize if an exiting
                // pathfinding search is in progress.
                return false;
            }
            Reset();
            Start = start;
            Goal = goal;
            float H = HeuristicCost(Start.Value, Goal.Value);
            PathFinderNode root = new PathFinderNode(Start, null, 0.0f, H);
            _openList.Add(root);
            _currentNode = root;
            onAddToOpenList?.Invoke(_currentNode);
            onChangeCurrentNode?.Invoke(_currentNode);
            Status = PathFinderStatus.RUNNING;
            return true;
        }

        public PathFinderStatus Step()
        {
            _closedList.Add(_currentNode);
            onAddToClosedList?.Invoke(_currentNode);
            if (_openList.Count == 0)
            {
                Status = PathFinderStatus.FAILURE;
                return Status;
            }
            _currentNode = GetLeastCostNode(_openList);
            onChangeCurrentNode?.Invoke(_currentNode);
            _openList.Remove(_currentNode);
            if (EqualityComparer<T>.Default.Equals(_currentNode.Location.Value, Goal.Value))
            {
                Status = PathFinderStatus.SUCCESS;
                onDestinationFound?.Invoke(_currentNode);
                return Status;
            }
            List<Node<T>> neighbours = _currentNode.Location.GetNeighbours();
            foreach (Node<T> cell in neighbours)
            {
                SpecificAlgorithm(cell);
            }

            Status = PathFinderStatus.RUNNING;
            return Status;
        }

        abstract protected void SpecificAlgorithm(Node<T> cell);

        public List<Node<T>> GetReversedPath()
        {
            if (Status != PathFinderStatus.SUCCESS)
            {
                Console.WriteLine("PathFinding Algorithm failed");
                //Debug.Log("")
                return null;
            }
            List<Node<T>> path = new List<Node<T>>();
            path.Add(CurrentNode.Location);
            PathFinderNode nextNode = CurrentNode.Parent;
            while(nextNode != null)
            {
                path.Add(nextNode.Location);
                nextNode = nextNode.Parent;
            }
            return path;
        }
    }
}
