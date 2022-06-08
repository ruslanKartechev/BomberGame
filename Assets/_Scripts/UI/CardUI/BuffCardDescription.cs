using System.Collections.Generic;
using UnityEngine;
namespace BomberGame.UI
{


    public class BuffCardDescription
    {
        private Card_Buff _card;
        private BuffUIDescriptionContainer _descriptions;

        public BuffCardDescription(Card_Buff card, BuffUIDescriptionContainer descriptions)
        {
            _card = card;
            _descriptions = descriptions;
        }

        public CardDescription GetDescription()
        {
            CardDescription description = new CardDescription();
            BuffDescription buffDesctiption = _descriptions.GetDescription(_card.Buff.TypeID);
            description.Title = buffDesctiption.Title;
            description.Duration = _card.Duration;
            description.GeneralDescription = buffDesctiption.GeneralDescription;
            description.SpecificDescription = buffDesctiption.SpecificDescription;
            description.SetValue = _card.Buff.GetStringValue();

            description.TargetsIDs = new List<string>(_card.Targets.Count);
            foreach (InteractableEntity target in _card.Targets)
            {
                description.TargetsIDs.Add(target.EntityID);
            }
            return description;
        }
    }


    
}