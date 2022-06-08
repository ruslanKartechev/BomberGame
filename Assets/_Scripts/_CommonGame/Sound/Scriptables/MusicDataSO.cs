using UnityEngine;


namespace CommonGame.Sound
{
    [CreateAssetMenu(fileName = "MusicDataSO", menuName = "SO/Sound/MusicDataSO", order = 1)]

    public class MusicDataSO: ScriptableObject
    {
        public MusicInfo mMusic;
    }

}