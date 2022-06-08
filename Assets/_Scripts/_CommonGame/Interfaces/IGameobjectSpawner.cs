using UnityEngine;

namespace CommonGame
{
    public interface IGameobjectSpawner
    {
        GameObject InstantiatePF(GameObject pf);
        GameObject InstantiatePF(GameObject pf, Transform parent);
        GameObject InstantiatePF(GameObject pf, Transform parent, Vector3 worldPos);

        void DestroyGO(GameObject go);
    }
}
