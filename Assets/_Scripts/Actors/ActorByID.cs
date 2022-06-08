namespace BomberGame
{
    public struct ActorByID
    {
        public string id;
        public InteractableEntity ActorEntity;

        public ActorByID(string id, InteractableEntity actor)
        {
            this.id = id;
            ActorEntity = actor;
        }
    }
}
