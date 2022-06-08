using System.Collections.Generic;
using System.Linq;
using System;
namespace CommonGame
{
    public class IObjectPool<T> where T : class
    {
        // pair <object-T, IsAvailable>
        public Dictionary<T, bool> Pool;
        public Action OnEmpty;
        public Action<T> OnTaken;
        public Action<T> OnReturned;
        public int MaxSize;
        public int CurrentSize;
        public IObjectPool(int MaxSize,  Action OnEmpty, Action<T> OnTaken, Action<T> OnReturned)
        {
            this.MaxSize = MaxSize;
            this.OnEmpty = OnEmpty;
            this.OnTaken = OnTaken;
            this.OnReturned = OnReturned;
            Pool = new Dictionary<T, bool>();
        }
        public bool AddToPool(T target)
        {
            if (CurrentSize >= MaxSize)
                return false;
            
            if (Pool.ContainsKey(target) == false)
            {
                Pool.Add(target, true);
            }
            CurrentSize++;
            return true;
        }

        public T TakeFromPool()
        {
            T res = Pool.FirstOrDefault(x => x.Value == true).Key;
            if (res == null)
            {
                OnEmpty?.Invoke();
                return res;
            }
            else
            {
                Pool[res] = false;
                OnTaken?.Invoke(res);
                return res;
            }
        }

        public bool ReturnToPool(T target)
        {
            if(Pool.ContainsKey(target) == false)
            {
                return false;
            }
            Pool[target] = true;
            OnReturned.Invoke(target);
            return true;
        }

        public void ClearPool()
        {
            Pool.Clear();
            CurrentSize = 0;
        }

    }
}