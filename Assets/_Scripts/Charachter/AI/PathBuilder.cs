using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{
    public class PathBuilder
    {
        protected Map _map;
        protected float _gridSize = 1;
        protected MapPositionValidator _positionValidator;
        protected PathFindStrategy _pathStrategy;

        public PathBuilder(Map map, MapPositionValidator positionValidator)
        {
            _map = map;
            _gridSize = _map.GetGridSize();
            _positionValidator = positionValidator;
        }

        public List<Vector2> GetPath(Vector2 start, Vector2 end)
        {
            _pathStrategy = new HorizontalPriorityMoveStrategy(_gridSize, _positionValidator);
            List<Vector2> path = _pathStrategy.GetPath(start, end);

            return path;
        }
    }


    public abstract class PathFindStrategy
    {
        public abstract List<Vector2> GetPath(Vector2 start, Vector2 end);
    }


    public class DepthFirstStratery
    {
        protected float _gridSize;
        protected Map map;
        protected MapBorders _borders;
        protected IPositionValidator _positionValidator;

        protected List<Vector2> _blocked;

        float _firstX;
        float _firstY;



        protected bool CheckRecursive(Vector2 current, Vector2 target)
        {
            bool canReach = false;
            if(_blocked.Contains(current) == true)
            {
                return false;
            }
            if(CheckPosition(current) == false)
            {
                return false;
            }

            Vector2 right = current + Vector2.right * _gridSize;
            Vector2 left = current - Vector2.right * _gridSize;
            Vector2 up = current + Vector2.up * _gridSize;
            Vector2 down = current - Vector2.up * _gridSize;
            if(CheckRecursive(right, target) == false)
            {
                return false;
            }
            CheckRecursive(left, target);
            CheckRecursive(up, target);
            CheckRecursive(down, target);

            return true;
        }



        protected bool CheckPosition(Vector2 position)
        {
            bool allow = _positionValidator.CheckPosition(position); ;
            return allow;
        }

    }


}