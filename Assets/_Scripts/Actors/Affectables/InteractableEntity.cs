using UnityEngine;
using BomberGame.Health;
namespace BomberGame
{
    public abstract class InteractableEntity : MonoBehaviour
    {
        [SerializeField] protected string _ID;
        [SerializeField] protected string _Kind;
        public string EntityID { get { return _ID; } }
        public string EntityKind { get { return _Kind; } }

        public abstract T GetEntityComponent<T>() where T : class;
        public abstract void AddEntityComponent<T>(object mObject) where T : class;
    }
}
