using UnityEngine;
using BomberGame.Health;
namespace BomberGame.UI
{

    public class ActorUIBlock : MonoBehaviour
    {
        [SerializeField] private ActorBlockMask _blockMask;
        [SerializeField] private ActorNameBlock _name;
        [SerializeField] private ActorAvatarBlock _avatar;
        [SerializeField] private ActorHealthBlock _health;
        [SerializeField] private ActorUIHighlighter _highlighter;
        public string ID { get; private set; }

        public void InitActorUI(IBomberActor actor)
        {
            ID = actor.EntityID;
            _name?.Init(actor.EntityID);
            _avatar?.Init(actor.GetEntityComponent<ISpriteView2D>());
            _health?.Init(actor.GetEntityComponent<IHealthComponent>());
            _blockMask?.ShowBlock();
            _highlighter?.Init();
        }

        public void HighlightPlayer(float duration)
        {
            _highlighter.Highlight(duration);
        }

        public void HighlightPlayer()
        {
            _highlighter.Highlight();
        }

        public void StopHighlight()
        {
            _highlighter.StopHighlight();
        }

    }

}