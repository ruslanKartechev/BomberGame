using System.Collections.Generic;
using CommonGame.RandomGen;
using UnityEngine;
namespace BomberGame
{
    [System.Serializable] 
    public class RandomBuffGenerator
    {
        public bool DebugLog = false;
        public List<WeightedParam<BuffContainer>> ContainerChoiceParams = new List<WeightedParam<BuffContainer>>();
        public BuffContainer GetRandomContainer()
        {
            WeightedChanceRandomizer<BuffContainer> randomizer = new WeightedChanceRandomizer<BuffContainer>(ContainerChoiceParams.ToArray());
            var container = randomizer.GetRandomResult();
            return container;
        }

        public BuffBase GetRandomBuff()
        {
            var container = GetRandomContainer();
            int index = UnityEngine.Random.Range(0, container.BuffsList.Count);
            var buff = container.BuffsList[index];
            if(DebugLog)
                Debug.Log("generated buff = " + buff.TypeID + " version: " + buff.VersionID);
            return buff;
        }

    }

}