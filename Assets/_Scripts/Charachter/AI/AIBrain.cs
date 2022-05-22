using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{

    public class AIBrain
    {
        private AIMovement _movement;
        private AIBomber _bomber;
        private Map _map;
        private AIBehaviour _currentBehaviour;

        public AIBrain(AIMovement movement, AIBomber bomber, Map map)
        {
            _movement = movement;
            _bomber = bomber;
            _map = map;
        }

        public void Enable()
        {
            _currentBehaviour = new WonderingBehaviour(_movement, _map);
            _currentBehaviour.StartBehaviour();
        }
        public void Disable()
        {
            _currentBehaviour?.StopBehaviour();
        }

    }


   

    public class AIBomber
    {





    }

}