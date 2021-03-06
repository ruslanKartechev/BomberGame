using UnityEngine;
using UnityEngine.Events;
using System;
namespace CommonGame.Events
{
    [CreateAssetMenu(fileName = "LevelLoadChannelSO", menuName = "EventChannels/LevelLoadChannelSO", order = 1)]

    public class LevelLoadChannelSO : ScriptableObject
    {
        public Action LoadNext;
        public Action LoadLast;
        public Action ReloadCurrentLevel;
        public Action<int> LoadLevel;
        public UnityAction<int> OnLevelLoaded;

        public void RaiseEventReload()
        {
            if (ReloadCurrentLevel != null)
            {
                ReloadCurrentLevel.Invoke();
            }
            else
            {
                Debug.Log("LoadNext action is null");
            }
        }
        public void RaiseEventLoadNext()
        {
            if (LoadNext != null)
            {
                LoadNext.Invoke();
            }
            else
            {
                Debug.Log("LoadNext action is null");
            }
        }
        public void RaiseEventLoadLast()
        {
            if (LoadLast != null)
            {
                LoadLast.Invoke();
            }
            else
            {
                Debug.Log("LoadNext action is null");
            }
        }
        public void RaiseEventLevelLoad(int levelIndex)
        {
            if (LoadLevel != null)
            {
                LoadLevel.Invoke(levelIndex);
            }
            else
            {
                Debug.Log("OnLevelLoad action is null");
            }
        }
        
    }
}