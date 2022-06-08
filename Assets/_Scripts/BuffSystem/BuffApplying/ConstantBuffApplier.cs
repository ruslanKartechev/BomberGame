using UnityEngine;

namespace BomberGame
{
    public class ConstantBuffApplier : MonoBehaviour
    {
        private bool OverridePrev = true;

        public virtual bool ApplyBuffToActor(BuffBase buff, InteractableEntity actor)
        {
            OverridePrev = buff.AllowOverridePrevious;
            try
            {
                if (CheckBuffConflict(actor.GetEntityComponent<ActiveBuffContainer>(), buff.TypeID))
                {
                    buff.Apply(actor);
                    return true;
                }
                else
                    return false;
            } catch { throw new System.Exception("Cannot check buff conflict"); }
        
        }

        protected bool CheckBuffConflict(ActiveBuffContainer container, string buffID)
        {
            
            if (container.ActiveBuffsTable.ContainsKey(buffID) == false || OverridePrev == true)
            {
                return true;
            }
            return false;
        }
    }
}