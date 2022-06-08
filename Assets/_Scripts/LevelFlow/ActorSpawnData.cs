namespace BomberGame
{
    [System.Serializable]
    public struct ActorSpawnData
    {
        public IBomberActor Actor;
        public SpawnSettings Settings;
    }
}
