using UnityEngine;
using CommonGame.RandomGen;
using System;
namespace BomberGame
{

    [System.Serializable]
    public class CardDurationGen
    {
        public float Duration_min;
        public float Duration_max;
        [Range(0f,1f)]public float UnitimedChance;
        
        private float Span { get { return (Duration_max - Duration_min); } }

        private System.Random _rand;
        private WeightedChanceRandomizer<TimeGetter> _randomizer;
        private delegate float TimeGetter();

        public void Init()
        {
            _rand = new System.Random();
            WeightedParam<TimeGetter>[] parameters = new WeightedParam<TimeGetter>[2];
            parameters[0] = new WeightedParam<TimeGetter>(Untimed, UnitimedChance);
            parameters[1] = new WeightedParam<TimeGetter>(RandomDuration, 1-UnitimedChance);
            _randomizer = new WeightedChanceRandomizer<TimeGetter>(parameters);
        }

        public float GetTime()
        {
            var callback = _randomizer.GetRandomResult();
            try
            {
                return callback.Invoke();

            }
            catch
            {
                Debug.Log("Cannot return time from random callback, returning -1");
                return -1;
            }
        }

        private float Untimed()
        {
            return -1;
        }

        private float RandomDuration()
        {
            return (float)_rand.NextDouble()*Span;
        }

    }

}