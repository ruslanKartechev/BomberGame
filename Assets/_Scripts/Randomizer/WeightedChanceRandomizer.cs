using System.Collections;
using UnityEngine;
using System.Linq;
using System;
namespace CommonGame.RandomGen
{
    [System.Serializable]
    public class WeightedParam<T>
    {
        public T Callback;
        public double Ratio;

        public WeightedParam(T callback, double chance)
        {
            Callback = callback;
            Ratio = chance;
        }
    }


    public class WeightedChanceRandomizer<T> 
    {
        public WeightedParam<T>[] Parameters { get; protected set; }
        private System.Random Random;
        public double RatioSum()
        {
            return Parameters.Sum(p => p.Ratio);
        }

        public WeightedChanceRandomizer(WeightedParam<T>[] weightedParams)
        {
            Parameters = weightedParams;
        }

        public T GetRandomResult()
        {
            Random = new System.Random();
            double randomVal = Random.NextDouble() * RatioSum();
            foreach(WeightedParam<T> p in Parameters)
            {
                randomVal -= p.Ratio;
                if (randomVal > 0)
                    continue;
                return p.Callback;
            }
            return default(T);
        }

    }
  
}