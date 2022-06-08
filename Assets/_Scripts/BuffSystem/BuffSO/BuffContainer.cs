using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    [CreateAssetMenu(fileName = "BuffContainer", menuName = "SO/Containers/BuffContainer", order = 1)]
    public class BuffContainer : ScriptableObject
    {
        public List<BuffBase> BuffsList = new List<BuffBase>();
        public BuffBase GetBuff(string id)
        {
            return BuffsList.Find(t => t.TypeID == id);
        }
        public BuffBase GetBuff(string id, string version)
        {
            return BuffsList.Find(t => t.TypeID == id && t.VersionID == version);
        }
    }
}