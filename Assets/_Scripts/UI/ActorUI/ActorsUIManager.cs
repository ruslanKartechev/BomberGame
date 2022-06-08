using System.Collections.Generic;
using UnityEngine;
namespace BomberGame.UI
{
    public class ActorsUIManager : MonoBehaviour
    {
        [SerializeField] private List<ActorUIBlock> _blockManagers = new List<ActorUIBlock>();
        private Dictionary<string, ActorUIBlock> _blocksTable = new Dictionary<string, ActorUIBlock>();
        public BombersInitializer _bomberInitializer;

        private void OnEnable()
        {
            _bomberInitializer.OnBombersInitialized += InitActorBlocks;
        }
        private void OnDisable()
        {
            _bomberInitializer.OnBombersInitialized -= InitActorBlocks;

        }

        public void InitActorBlocks(List<IBomberActor> actors)
        {
            for(int i = 0; i < _blockManagers.Count && i < actors.Count; i++)
            {
                _blockManagers[i].InitActorUI(actors[i]);
                _blocksTable.Add(actors[i].EntityID, _blockManagers[i]);
            }
        }

        public void Hightlight(string id, float duration)
        {
            ActorUIBlock block;
            if (_blocksTable.TryGetValue(id, out block))
            {
                block.HighlightPlayer(duration);
            }
        }

        public void Highlight(string id)
        {
            ActorUIBlock block;
            if (_blocksTable.TryGetValue(id, out block))
            {
                block.HighlightPlayer();
            }
        }

        public void StopHighlight(string id)
        {
            ActorUIBlock block;
            if (_blocksTable.TryGetValue(id, out block))
            {
                block.StopHighlight();
            }
        }

        public void StopHightlightAll()
        {
            foreach (ActorUIBlock block in _blockManagers)
            {
                block.StopHighlight();
            }
        }

    }

}