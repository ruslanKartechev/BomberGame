using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CommonGame.RandomGen;

namespace BomberGame
{

    [System.Serializable]
    public class CardTargetGen
    {
        public float ChanceOne;
        public float ChanceTwo;
        public float ChanceThree;
        public float ChanceEveryone;

        private int GetCount(int totalCount)
        {
            WeightedParam<int>[] parameters = new WeightedParam<int>[4];
            parameters[0] = new WeightedParam<int>(1, ChanceOne);
            parameters[1] = new WeightedParam<int>(2, ChanceTwo);
            parameters[2] = new WeightedParam<int>(3, ChanceThree);
            parameters[3] = new WeightedParam<int>(totalCount, ChanceEveryone);
            WeightedChanceRandomizer<int> countGen = new WeightedChanceRandomizer<int>(parameters);
            return countGen.GetRandomResult();
        }

        public List<InteractableEntity> GetRandomTargets(int totalCount, List<InteractableEntity> chooseFrom)
        {
            int count = GetCount(totalCount);
            
            if(count < totalCount)
            {
                try
                {
                    List<InteractableEntity> result = RandomizeHelpers.GetRandomFromList(chooseFrom, count);
                    return result;
                }
                catch(Exception ex)
                {
                    Debug.Log("Exception: " + ex.Message + "\n Returning full from list");
                    return chooseFrom;
                }
            }
            else
            {
                return chooseFrom;
            }
        }




    }

}