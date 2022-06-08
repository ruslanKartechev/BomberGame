using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
namespace BomberGame
{
    public class BuffManager : MonoBehaviour
    {
        [Inject] private BuffContainer _buffs;
        [Inject] private ActorsContainer _actorContainer;
        //[SerializeField] private ConstantBuffApplier _constantBuffApplier;
        [SerializeField] private TimedBuffApplier _buffApplier;


        public void ApplyBuff(BuffBase buff, string actorID)
        {
            try
            {
                var target = GetTarget(actorID);
                _buffApplier.ApplyBuffToActor(buff, target);

            }
            catch (System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
        }

        public void ApplyTimedBuff(BuffBase buff, string actorID, float duration)
        {
            try
            {
                var target = GetTarget(actorID);
                _buffApplier.ApplyTimed(buff, target, duration);

            }
            catch (System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
        }

        public void ApplyBuff(string buffID, string version,string actorID)
        {
            try
            {
                var pair = GetBuffTargetPair(buffID, version, actorID);
                _buffApplier.ApplyBuffToActor(pair.Item1, pair.Item2);
            }
            catch (System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
        }

        public void ApplyTimedBuff(string buffID, string version, string actorID, float duration)
        {
            try
            {
                var pair = GetBuffTargetPair(buffID, version, actorID);
                _buffApplier.ApplyTimed(pair.Item1, pair.Item2, duration);
            }
            catch(System.Exception ex)
            {
                Debug.Log($"Caught: {ex.Message}");
            }
         
        }

        private (BuffBase, InteractableEntity) GetBuffTargetPair(string buffID, string version, string actorID)
        {
            InteractableEntity target = GetTarget(actorID);
            BuffBase buff = GetBuffFromContainer(buffID, version);
            if (target == null)
            {
                throw new System.Exception($"Actor with id {actorID} was not found");
            }
            if (buff == null)
            {
               throw new System.Exception($"Buff with id {actorID} ver: {version} was not found");
            }
            return (buff, target);
        }


        private BuffBase GetBuffFromContainer(string id, string version)
        {
            BuffBase buff = _buffs.GetBuff(id,version);
            return buff;
        }

        private InteractableEntity GetTarget(string id)
        {
            return _actorContainer.GetActor(id);
        }

    }
}