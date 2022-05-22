using System;
using UnityEngine;


namespace CommonGame.Sound
{
    [CreateAssetMenu(fileName = "SoundFXChannelSO", menuName = "EventChannels/SoundFXChannelSO", order = 1)]

    public class SoundFXChannelSO : ScriptableObject
    {
        public Action<string> PlayFX;
        public Action<string> PlayLoopedFX;
        public Action<string> StopPlayingLoop;

        public void RaisePlay(string name)
        {
            if (PlayFX != null)
                PlayFX?.Invoke(name);
            else
                Debug.Log("OnPlayFX action is null");
        }
        public void RaiseStartLoop(string name)
        {
            if (PlayLoopedFX != null)
                PlayLoopedFX?.Invoke(name);
            else
                Debug.Log("OnPlayFXLoop action is null");
        }
        public void RaiseStopLoop(string name)
        {
            if (StopPlayingLoop != null)
                StopPlayingLoop?.Invoke(name);
            else
                Debug.Log("OnStopFXLoop action is null");
        }

    }
}