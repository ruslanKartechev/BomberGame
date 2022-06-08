using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BomberGame {


    [System.Serializable]
    public struct BuffDescription
    {
        public string BuffTypeID;
        public string Title;
        public string GeneralDescription;
        public string SpecificDescription;
    }


    [CreateAssetMenu(fileName = "BuffUIDescriptionContainer", menuName = "SO/UI/BuffUIDescriptionContainer")]
    public class BuffUIDescriptionContainer : ScriptableObject
    {
        public List<BuffDescription> Descriptions = new List<BuffDescription>();
        public BuffDescription GetDescription(string buffTypeID)
        {
            return Descriptions.Find(t => t.BuffTypeID == buffTypeID);
        }
    }
}