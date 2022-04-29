
using UnityEngine;

namespace CommonGame.Controlls
{
    [CreateAssetMenu(fileName = "InputEventChannelSO", menuName = "InputEvents/InputEventChannelSO", order = 1)]
    public class InputMoveChannelSO : ScriptableObject
    {
        public event Notifier Up;
        public event Notifier Down;
        public event Notifier Right;
        public event Notifier Left;
        public void RaiseEventUp()
        {
            if (Up != null)
                Up.Invoke();
        }
        public void RaiseEventDown()
        {
            if (Down != null)
                Down.Invoke();
        }
        public void RaiseEventLeft()
        {
            if (Left != null)
                Left.Invoke();
        }
        public void RaiseEventRight()
        {
            if (Right != null)
                Right.Invoke();
        }

    }

    public delegate void Notifier();
}