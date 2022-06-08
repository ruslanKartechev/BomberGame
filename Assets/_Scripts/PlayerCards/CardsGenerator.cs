using System.Collections;
using UnityEngine;
using BomberGame.UI;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace BomberGame
{
    public enum CardTypes { PlayerBuffCard, PlayerGiftCard, ObstacleCard }

    public class CardsGenerator : MonoBehaviour
    {
        [SerializeField] private CardGenerationSettings _cardGenerationSettings;
        [Space(20)]
        [SerializeField] private Generator_BuffCard _buffCardGenerator;
        [SerializeField] private BuffUIDescriptionContainer _buffsDescriptions;
        [SerializeField] private BuffManager _buffSystem;
        [Space(20)]
        [SerializeField] private CardViewManager _cardUI;
        
        private CancellationTokenSource _tokenSource;

        [System.Serializable]
        public struct CardGenerationSettings
        {
            public TimingSettings Timing;
        }

        private void Start()
        {
            _buffCardGenerator.Init();
        }


        private void OnDisable()
        {
            DisableGenerator();
        }

        public void EnableGenerator()
        {
            _tokenSource?.Cancel();
            _tokenSource = new CancellationTokenSource();
            GenerationProcess(_tokenSource.Token);
        }

        public void DisableGenerator()
        {
            _tokenSource?.Cancel();
        }
        
        public async void GenerationProcess(CancellationToken token)
        {
            while (token.IsCancellationRequested == false)
            {
                await GenerateRandomCard(token);
                await Task.Delay(1000 * (int)(GetDelay()));
            }
        }

        private float GetDelay()
        {
            return _cardGenerationSettings.Timing.GetRandomDelay();
        }

        public async Task GenerateRandomCard(CancellationToken token)
        {
            CardTypes card_type = GetRandomType();
            try
            {
                switch (card_type)
                {
                    case CardTypes.PlayerBuffCard:
                        await GeneratePlayerCard(token);
                        break;
                }
            }
            catch(Exception ex)
            {
                Debug.Log($"Stopped generating cards. Exception {ex.Message}  {ex.StackTrace}");
                return;
            }
        }

        private CardTypes GetRandomType()
        {
            return CardTypes.PlayerBuffCard;
        }

        #region BuffCard
        private async Task GeneratePlayerCard(CancellationToken token)
        {
            Card_Buff card = _buffCardGenerator.GetRandomBuffCard();
            SetDescription(card);
            bool applyCard = await WaitForInput(token);
            token.ThrowIfCancellationRequested();
            if (applyCard)
            {
                ApplyPlayerCard(card);
            }
        }

        private void SetDescription(Card_Buff card)
        {
            try
            {
                BuffCardDescription cardDescription = new BuffCardDescription(card, _buffsDescriptions);
                var description = cardDescription.GetDescription();
                try
                {
                    _cardUI.SetBlockData(description);
                }
                catch { Debug.Log("Problem assigning cardUI block"); }
            }
            catch { Debug.Log("Cannot generate description for the card"); }
        }

        private void ApplyPlayerCard(Card_Buff card)
        {
            try
            {
                ApplyBuffs(card);
            }
            catch
            {
                Debug.Log("Cannot apply player card");
            }
        }

        private void ApplyBuffs(Card_Buff card)
        {
            foreach (InteractableEntity entity in card.Targets)
            {
                if(card.Duration == -1)
                    _buffSystem.ApplyBuff(card.Buff, entity.EntityID);
                else
                    _buffSystem.ApplyTimedBuff(card.Buff, entity.EntityID, card.Duration);
            }
        }
        #endregion


        private async Task<bool> WaitForInput(CancellationToken token)
        {
            try
            {
                bool applyCard = await _cardUI.WaitForPlayerChoice(token);
                return applyCard;
            }
            catch
            {
                Debug.Log("Cannot wait for input from UI");
                return false;
            }
        }


    }


}