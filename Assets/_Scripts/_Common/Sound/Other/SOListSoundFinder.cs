using System.Collections.Generic;
using UnityEngine;
namespace CommonGame.Sound
{
    [System.Serializable]
    public class SOListSoundFinder : ISoundFinder
    {
        private Dictionary<string, SoundInfo> _soundTable;
        private Dictionary<string, LoopedSoundSO> _loopSoundTable;

        public void Init(List<SoundSO> sounds, List<LoopedSoundSO> loopedSounds)
        {
            _soundTable = new Dictionary<string, SoundInfo>();
            foreach (SoundSO so in sounds)
            {
                _soundTable.Add(so.mSoundInfo.Name, so.mSoundInfo);
            }

            _loopSoundTable = new Dictionary<string, LoopedSoundSO>();
            foreach (LoopedSoundSO so in loopedSounds)
            {
                _loopSoundTable.Add(so.mSoundInfo.Name, so);
                _soundTable.Add(so.mSoundInfo.Name, so.mSoundInfo);
            }
        }

        public SoundInfo GetSound(string name)
        {
            if (_soundTable.ContainsKey(name) == false)
            {
                Debug.Log($"Clip {name} not found");
                return new SoundInfo();
            }
            SoundInfo sound = _soundTable[name];
            return sound;
        }

        public (SoundInfo, LoopedSoundinfo) GetLoopSound(string name)
        {
            if (_loopSoundTable.ContainsKey(name) == true)
            {
                SoundInfo info = _loopSoundTable[name].mSoundInfo;
                LoopedSoundinfo loopinfo = _loopSoundTable[name].mLoopInfo;
                return (info, loopinfo);
            }
            else
                return (new SoundInfo(), new LoopedSoundinfo());
        }
    }
}