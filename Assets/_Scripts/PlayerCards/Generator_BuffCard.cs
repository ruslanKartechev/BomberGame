using System.Collections.Generic;
using UnityEngine;
using System;
namespace BomberGame
{


    [System.Serializable]
    public class Generator_BuffCard
    {
        [SerializeField] private RandomBuffGenerator _randomBuffGen;
        [SerializeField] private ActorsContainer _actorContainer;
        [Space(10)]
        [SerializeField] private CardDurationGen _durationGen;
        [SerializeField] private CardTargetGen _randomTargetGen;

        public void Init()
        {
            _durationGen.Init();
        }

        public Card_Buff GetRandomBuffCard()
        {
            Card_Buff card = new Card_Buff();
            card.Buff = GetRandomBuff();
            card.Duration = GetRandomDuration();
            card.Targets = GetRandomTarget();
            return card;
        }

        private BuffBase GetRandomBuff()
        {
            try
            {
                return _randomBuffGen.GetRandomBuff();
            }
            catch(Exception ex)
            {
                Debug.Log($"Cannot Generate random buff for the card {ex.Message}");
                return null;
            }
        }

        private int GetRandomDuration()
        {
            var duration = _durationGen.GetTime();
            return Mathf.RoundToInt(duration);
        }

        private List<InteractableEntity> GetRandomTarget()
        {
            var potentialTargets = _actorContainer.GetAllActors();
            List<InteractableEntity> entityList = new List<InteractableEntity>(potentialTargets.Count);
            foreach(ActorByID actor in potentialTargets)
            {
                entityList.Add(actor.ActorEntity);
            }
            List<InteractableEntity> targets = _randomTargetGen.GetRandomTargets(entityList.Count, entityList);
           
            return targets;
        }


    }

}