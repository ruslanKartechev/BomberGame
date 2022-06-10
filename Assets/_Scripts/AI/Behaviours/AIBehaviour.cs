using System;
namespace BomberGame
{
    public abstract class AIBehaviour
    {
        public abstract void StartBehaviour();
        public abstract void Abort();
        public Action OnBehaviourFinished;
    }

}