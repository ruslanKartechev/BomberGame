namespace BomberGame
{
    public class AStarPathFinder<T> : PathFinder<T>
    {
        protected override void SpecificAlgorithm(Node<T> node)
        {

            if (CheckInList(_closedList, node.Value) == -1)
            {
                float G = _currentNode.Gcost +
                  NodeTravelCost(_currentNode.Location.Value, node.Value);
                float H = HeuristicCost(node.Value, Goal.Value);
                int id_openList = CheckInList(_openList, node.Value);
                if (id_openList == -1)
                {
                    PathFinderNode n = new PathFinderNode(node, _currentNode, G, H);
                    _openList.Add(n);

                    // call the delegate
                    onAddToOpenList?.Invoke(n);
                }
                else
                {
                    float oldG = _openList[id_openList].Gcost;
                    if (G < oldG)
                    {
                        _openList[id_openList].SetGcost(G);
                        _openList[id_openList].Parent = _currentNode;
                        onAddToOpenList?.Invoke(_openList[id_openList]);
                    }
                }
            }
        }

    }
}
