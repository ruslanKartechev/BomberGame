using System.Collections.Generic;
using System.Linq;
using UnityEngine;
namespace BomberGame
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPos"> - position parameter</typeparam>
    /// <typeparam name="TEntry"> - target object parameter</typeparam>
    public class MapNonMovableTable<TPos, TEntry>
    {
        private Dictionary<TEntry, TPos> _mainContainer;
        private Dictionary<TPos, TEntry> _inverseContainer;

        public MapNonMovableTable()
        {
            _mainContainer = new Dictionary<TEntry, TPos>();
            _inverseContainer = new Dictionary<TPos, TEntry>();
        }

        public void AddEntry(TEntry entry, TPos position)
        {
            try
            {
                _mainContainer.Add(entry, position);
                _inverseContainer.Add(position, entry);
            }
            catch(System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
        }

        public void RemoveEntry(TEntry entry)
        {
            try
            {
                var pos = _mainContainer[entry];
                _inverseContainer.Remove(pos);
                _mainContainer.Remove(entry);
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
        }

        public TEntry TryGetAt(TPos position)
        {
            TEntry entry;
            if (_inverseContainer.TryGetValue(position, out entry))
            {
                return entry;
            }
            else
                return default(TEntry);
        }

        public List<TEntry> GetAllEntries()
        {
            return _mainContainer.Keys.ToList();
        }
    }


}