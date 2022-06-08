using UnityEngine;
using System.Collections;
namespace BomberGame
{
    public class AIOnlyLevelFlowManager : LevelFlowManager
    {
        [SerializeField] private CardsGenerator _cardsManager;
        [SerializeField] private BombersInitializer _bomberInitializer;
        [SerializeField] private float _delayBeforeFirstCard;
        public override void Init()
        {
            StartCoroutine(InitRoutine());
        }

        private IEnumerator InitRoutine()
        {
            ActivateBombers();
            yield return new WaitForSeconds(_delayBeforeFirstCard);
            _cardsManager.EnableGenerator();
        }

        private void ActivateBombers()
        {
            foreach(IBomberActor bomber in _bomberInitializer.InitializedBombers)
            {
                bomber.SetActive();
            }
        }
    }
}