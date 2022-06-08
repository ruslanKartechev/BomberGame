using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame
{
    public abstract class CardBase
    {
        public string CardTypeID;
        /// <summary>
        /// Duration of the card effect in seconds, -1 if applied forever
        /// </summary>
        public int Duration;
        public List<InteractableEntity> Targets;
    }

    public abstract class PlayerGiveCard : CardBase
    {

    }

    public abstract class WallsCard : CardBase
    {

    }

    
}