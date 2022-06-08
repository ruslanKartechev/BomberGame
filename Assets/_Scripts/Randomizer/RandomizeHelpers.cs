using System.Collections.Generic;
namespace CommonGame.RandomGen
{
    public class RandomizeHelpers
    {
        public static List<T> GetRandomFromList<T>(List<T> fromList, int count)
        {
            if (count >= fromList.Count)
            {
                throw new System.Exception("Choose count >= fromlist count");
            }
            List<int> openList = new List<int>(fromList.Count);
            List<int> closedList = new List<int>(count);

            for (int i = 0; i < fromList.Count; i++)
            {
                openList.Add(i);
            }

            for (int i = 0; i < count; i++)
            {
                int ind = 0;
                do
                {
                    ind = UnityEngine.Random.Range(0, openList.Count);
                } while (openList.Contains(ind) == false);
                openList.Remove(ind);
                closedList.Add(ind);
            }

            List<T> output = new List<T>(count);

            for (int i = 0; i < closedList.Count; i++)
            {
                output.Add(fromList[closedList[i]]);
            }

            return output;
        }


        public static T GetRandomFromList<T>(List<T> items)
        {
            int index = UnityEngine.Random.Range(0,items.Count);
            return items[index];
        }

    }


}