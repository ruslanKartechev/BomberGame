using UnityEngine;

namespace CommonGame
{
    [CreateAssetMenu(fileName = "GOSpawnerGetter", menuName = "SO/GOSpawnerGetter", order = 1)]
    public class GOSpawnerGetter : ScriptableObject
    {
        protected IGameobjectSpawner _spawner;

        public void SetSpawner(IGameobjectSpawner spawner)
        {
            _spawner = spawner;
        }
        public IGameobjectSpawner GetSpawner()
        {
            return _spawner;
        }
    }


}
