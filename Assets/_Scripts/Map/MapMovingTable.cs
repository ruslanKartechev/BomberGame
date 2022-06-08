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
    public class MapMovingTable<TPos, TEntry> 
    {
        private Dictionary<TEntry, TPos> _mainContainer;
        public Dictionary<TEntry, TPos> PositionTable { get { return _mainContainer; } }
        public MapMovingTable()
        {
            _mainContainer = new Dictionary<TEntry, TPos>();
        }

        public void AddEntry(TEntry entry, TPos posiiton)
        {
            try
            {
                _mainContainer.Add(entry, posiiton);
            }
            catch(System.Exception ex)
            {
                Debug.Log("Caught: " + ex.Message);
            }
        }

        public void RemoveEntry(TEntry entry)
        {
            try
            {
                _mainContainer.Remove(entry);
            }
            catch (System.Exception ex)
            {
                Debug.Log("Caught: " + ex.Message);
            }
        }

        public void UpdateEntryPosition(TEntry entry, TPos position)
        {
            try
            {
                _mainContainer[entry] = position;
            }
            catch (System.Exception ex)
            {
                Debug.Log("Caught: " + ex.Message);
            }
        }

        public List<TEntry> GetAllEntries()
        {
            return _mainContainer.Keys.ToList();
        }
      

        public List<TEntry> GetEntriesAt(TPos position)
        {
            List<TEntry> targets = new List<TEntry>();
            try
            {
                _mainContainer.All(t => t.Value.Equals(position));

            }catch(System.Exception ex)
            {
                Debug.Log("Caught: " + ex.Message);
            }
            return targets;
        }

    }


}