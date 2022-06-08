using System.Collections.Generic;

namespace BomberGame
{
    public class ActiveBuffContainer
    {
        protected Dictionary<string, BuffBase> _activeBuffsTable = new Dictionary<string, BuffBase>();
        public Dictionary<string, BuffBase> ActiveBuffsTable { get => _activeBuffsTable; set => _activeBuffsTable = value; }
        public void AddBuff(BuffBase buff)
        {
            _activeBuffsTable.Add(buff.TypeID, buff);
        }
    }
}