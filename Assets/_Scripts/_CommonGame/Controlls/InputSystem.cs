using CommonGame.Events;
using UnityEngine;
namespace CommonGame.Controlls
{
    public class InputSystem : MonoBehaviour, IInputSystem
    {
        [SerializeField] private LevelStartChannelSO _levelStartChannel;
        [SerializeField] private LevelFinishChannelSO _levelFinishChannel;

        [SerializeField] protected ControllManagerBase _manager;
        [SerializeField] private bool IsDebug = true;
        public void Init()
        {
            Input.simulateMouseWithTouches = true;
            if (IsDebug)
            {
                _manager.Init(null);
                _manager.EnableControlls();
                return;
            }
            _manager.Init(null);
            _levelFinishChannel.OnLevelFinished += Enable;
            _levelStartChannel.OnLevelStarted.AddListener(Disable);
        }


        public void Enable()
        {
            _manager.EnableControlls();
        }
        public void Disable()
        {
            _manager.DisableControlls();
        }


    }
}