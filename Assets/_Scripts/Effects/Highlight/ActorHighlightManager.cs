using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BomberGame.UI;


public interface IHighlightable
{
    void Highlight();
    void StopHighlight();
}
namespace BomberGame {

    public class ActorHighlightManager : MonoBehaviour
    {
        [SerializeField] private ActorsUIManager _bomberUI;
        [SerializeField] private IBomberActor _debugAddActor;
        private Dictionary<string, IHighlightable> _bombers = new Dictionary<string, IHighlightable>();
        public BombersInitializer _bomberInitializer;

        private void Start()
        {
            if(_debugAddActor!=null)
                AddBomber(_debugAddActor);
        }
        private void OnEnable()
        {
            _bomberInitializer.OnBombersInitialized += InitBombers;
        }
        private void OnDisable()
        {
            _bomberInitializer.OnBombersInitialized -= InitBombers;
        }

        private void InitBombers(List<IBomberActor> actors)
        {
            foreach(IBomberActor bomber in actors)
            {
                AddBomber(bomber);
            }
        }

        public void AddBomber(IBomberActor entity)
        {
            IHighlightable highlightable = entity.GetEntityComponent<IHighlightable>();
            
            if(highlightable != null)
            {
                _bombers.Add(entity.EntityID, highlightable);
            }
        }

        public void Highlight(List<string> ids)
        {
            foreach (string id in ids)
            {
                try
                {
                    _bomberUI.Highlight(id);
                }
                catch { Debug.Log("cannot highlight bomber UI"); }
                try
                {
                    _bombers[id].Highlight();
                }
                catch { Debug.Log("cannot HIGHLIGHT Bomber"); }
            }
        }

        public void StopHighlightAll()
        {
            foreach (string id in _bombers.Keys)
            {
                _bomberUI.StopHighlight(id);
                _bombers[id].StopHighlight();
            }
        }

        public void StopHighlight(List<string> ids)
        {
            foreach (string id in ids)
            {
                _bomberUI.StopHighlight(id);
                _bombers[id].StopHighlight();
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}