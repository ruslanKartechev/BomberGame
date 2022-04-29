using CommonGame.Events;
using UnityEngine;
namespace CommonGame.Controlls
{
    public class InputController : MonoBehaviour
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
            _levelFinishChannel.OnLevelFinished += OnLevelEnd;
            _levelStartChannel.OnLevelStarted.AddListener(OnLevelStarted);

        }
        protected void OnLevelStarted()
        {


        }
        protected void OnLevelEnd()
        {

        }
    }
}