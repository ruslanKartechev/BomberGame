using UnityEngine;
namespace BomberGame
{
    public class HorFirstMazeStrategy : VertFirstMazeStrategy
    {
        public HorFirstMazeStrategy(float gridSize, IPositionValidator positionCheck) : base(gridSize,positionCheck)
        {
        }

        protected override void TryFindDefaultCase(Vector2 end)
        {
            DebugText("1");
            if (CheckHorizontal(_mockCurrent, end, false) == false)
            {
                DebugText("2");
                if (CheckVertical(_mockCurrent, end, true) == false)
                {
                    DebugText("3");
                    if (CheckHorizontalFlipped(_mockCurrent, end) == false)
                    {
                        DebugText("4");
                        if (CheckVerticalFlipped(_mockCurrent, end) == false)
                        {
                            DebugText("5");
                            Debug.Log("<color=red> Tried all 4 directions, allow to move backwards </color>");
                            TryBackwards();
                        }
                    }
                }
            }
        }
        protected override void DebugText(string text)
        {

        }

    }


}