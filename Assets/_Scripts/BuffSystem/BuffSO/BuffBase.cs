using System;
using System.Collections;
using UnityEngine;

namespace BomberGame
{

    public abstract class BuffBase: ScriptableObject
    {
        public string TypeID;
        public string VersionID;
        public bool AllowOverridePrevious = true;

        public abstract void Apply(InteractableEntity target);
        public abstract void StopApply(InteractableEntity target);
        public abstract string GetStringValue();
        protected T TryGetTarget<T>(InteractableEntity target) where T : class
        {
            T res = target.GetEntityComponent<T>();
            if (res == null)
                throw new Exception("Couldn't get component");
            return res;

        }
    }

    

}