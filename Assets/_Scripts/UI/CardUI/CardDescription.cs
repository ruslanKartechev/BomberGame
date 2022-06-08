using System.Collections.Generic;
namespace BomberGame.UI
{
    public struct CardDescription
    {
        public string Title;
        public string GeneralDescription;
        public string SpecificDescription;
        public string SetValue;
        public int Duration;
        public List<string> TargetsIDs;

        public CardDescription(string title, string generalDescription, string specificDescription, string setValue, int duration, List<string> targetsID)
        {
            Title = title;
            GeneralDescription = generalDescription;
            SpecificDescription = specificDescription;
            SetValue = setValue;
            Duration = duration;
            TargetsIDs = targetsID;
        }
    }


    
}