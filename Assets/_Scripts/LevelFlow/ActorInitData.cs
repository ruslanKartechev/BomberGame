namespace BomberGame
{
    [System.Serializable]
    public class ActorInitData
    {
        public IBomberActor _actor;
        public string _name;
        public void InitActor()
        {
            _actor.Init();
        }
    }
}
