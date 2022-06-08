using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
namespace BomberGame
{


    public class VertFirstMazeStrategy : PathFindStrategy
    {
        protected List<Vector2> _path = new List<Vector2>();
        protected List<Vector2> _prevSteps = new List<Vector2>();
        protected Vector2 _mockCurrent;
        protected float _gridSize = 1;
        protected IPositionValidator _positionValidator;

        public VertFirstMazeStrategy(float gridSize , IPositionValidator positionCheck)
        {
            _path = new List<Vector2>() ;
            _prevSteps = new List<Vector2>();
            _gridSize = gridSize;
            _positionValidator = positionCheck;
        }
        public override async Task<List<Vector2>> GetPath(Vector2 start, Vector2 end)
        {
            _prevSteps.Clear();
            _prevSteps.Add(start);
            _path.Clear();
            _mockCurrent = start;
            int i = 0;
            int limit = 25;
            while (i < limit && _mockCurrent != end)
            {
                bool sameVert = (_mockCurrent.y == end.y) ? true : false;
                bool sameHor = (_mockCurrent.x == end.x) ? true : false;
                if (!sameHor && !sameVert)
                {
                    TryFindDefaultCase(end);
                }
                else if (sameHor)
                {
                    TryFindHorCase(end);
                }
                else if (sameVert)
                {
                    TryFindVertCase(end);
                }
                i++;
            }

            if (_path.Count > 1 && end == _path[_path.Count - 1])
            {
                Debug.Log("<color=green>Target will be reached</color>");
            }
            else
                Debug.Log($"<color=red>Ends not match, iterations limit reached: i =={i}</color>");
            return _path;
        }

        #region Cases
        protected virtual void TryFindDefaultCase(Vector2 end)
        {
            DebugText("1");
            if (CheckVertical(_mockCurrent, end, false) == false)
            {
                DebugText("2");

                if (CheckHorizontal(_mockCurrent, end, true) == false)
                {
                    DebugText("3");

                    if (CheckVerticalFlipped(_mockCurrent, end) == false)
                    {
                        DebugText("4");

                        if (CheckHorizontalFlipped(_mockCurrent, end) == false)
                        {
                            DebugText("5");

                            Debug.Log("<color=red> Tried all 4 directions, allow to move backwards </color>");
                            TryBackwards();
                        }
                    }
                }
            }
        }
        protected virtual void DebugText(string text)
        {
            //Debug.Log(text);
        }

        protected virtual void TryFindHorCase(Vector2 end)
        {
            if (CheckVertical(_mockCurrent, end, false) == false)
            {
                if (TryHorAnyDir(_mockCurrent, end) == false)
                {
                    Debug.Log("<color=red> Tried all 4 directions (SAME HOR CASE), allow to move backwards </color>");
                    TryBackwards();
                }
            }
        }

        protected virtual void TryFindVertCase(Vector2 end)
        {
            if (CheckHorizontal(_mockCurrent, end, false) == false)
            {
                if (TryVertAnyDir(_mockCurrent, end) == false)
                {
                    Debug.Log("<color=red> Tried all 4 directions (SAME Vert CASE), allow to move backwards </color>");
                    TryBackwards();
                }
            }
        }
        #endregion

        public bool CheckIfAvailable(Vector2 position)
        {
            if (_prevSteps.Contains(position))
                return false;
            bool allow = _positionValidator.CheckPositionSimple(position);
            if (allow == true)
            {
                Debug.Log($"allowed {position}");
                _prevSteps.Add(_mockCurrent);
                _path.Add(position);
                _mockCurrent = position;
            }
            return allow;
        }

        #region CheckNextPosition
        protected bool CheckVertical(Vector2 current, Vector2 target, bool checkPrev)
        {
            Vector2 tryPos = current;
            Vector2 prev = _prevSteps[_prevSteps.Count - 1];

            if (target.y > current.y)
            {
                tryPos = current + _gridSize * Vector2.up;
                if (checkPrev &&  tryPos == prev)
                {
                    Debug.Log("prev same as up");
                    tryPos = current - _gridSize * Vector2.up;

                }
            }
            else if (target.y < current.y)
            {
                tryPos = current - _gridSize * Vector2.up;
                if (checkPrev &&  tryPos == prev)
                {
                    Debug.Log("prev same as down");
                    tryPos = current + _gridSize * Vector2.up;
                }
            }
            bool allow = CheckIfAvailable(tryPos);
            return allow;
        }

        protected bool CheckVerticalFlipped(Vector2 current, Vector2 target)
        {
            Vector2 tryPos = current;
            if (target.y < current.y)
                tryPos = current + _gridSize * Vector2.up;
            else if (target.y > current.y)
                tryPos = current - _gridSize * Vector2.up;

            bool allow = CheckIfAvailable(tryPos);
            return allow;
        }

        protected bool TryVertAnyDir(Vector2 current, Vector2 target)
        {
            Vector2 tryPos = current;
            tryPos = current + _gridSize * Vector2.up;
            bool allow = CheckIfAvailable(tryPos);
            if (allow == false)
            {
                tryPos = current - _gridSize * Vector2.up;
                allow = CheckIfAvailable(tryPos);
            }
            return allow;
        }

        protected bool CheckHorizontal(Vector2 current, Vector2 target, bool checkPrev)
        {
            Vector2 tryPos = current;
            Vector2 prev = _prevSteps[_prevSteps.Count - 1];
            if (target.x > current.x)
            {
                tryPos = current + _gridSize * Vector2.right;
                if (checkPrev && prev == tryPos)
                {
                    Debug.Log("prev equal to right");
                    tryPos = current - _gridSize * Vector2.right;
                }
            }
            else if (target.x < current.x)
            {
                tryPos = current - _gridSize * Vector2.right;
                if (checkPrev &&  prev == tryPos)
                {
                    Debug.Log("prev equal to left");
                    tryPos = current + _gridSize * Vector2.right;
                }
            }
            bool allow = CheckIfAvailable(tryPos);
            return allow;
        }

        protected bool CheckHorizontalFlipped(Vector2 current, Vector2 target)
        {
            Vector2 tryPos = current;
            if (target.x < current.x)
                tryPos = current + _gridSize * Vector2.right;
            else if (target.x > current.x)
                tryPos = current - _gridSize * Vector2.right;
            bool allow = CheckIfAvailable(tryPos);
            return allow;
        }

        protected bool TryHorAnyDir(Vector2 current, Vector2 target)
        {
            Vector2 tryPos = current;
            tryPos = current + _gridSize * Vector2.right;
            bool allow = CheckIfAvailable(tryPos);
            if (allow == false)
            {
                tryPos = current - _gridSize * Vector2.right;
                allow = CheckIfAvailable(tryPos);
            }

            return allow;
        }

        #endregion

        protected void TryBackwards()
        {
            Vector2 current = _mockCurrent;
            Vector2 prev = _prevSteps[_prevSteps.Count - 1];
            _prevSteps.Clear();
            _mockCurrent = prev;
            _path.Add(prev);
            _prevSteps.Add(current);

        }

        public override void Stop()
        {
            throw new System.NotImplementedException();
        }
    }


}