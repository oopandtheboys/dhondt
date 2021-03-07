using System;
using System.Collections.Generic;
using System.Linq;

namespace D_Hondt
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }

        private static Dictionary<string, int> CalculateMethod(Dictionary<string, int> parties, int rounds)
        {
            // Parties has an int component and a string component.
            Dictionary<string, int> roundsWon = new Dictionary<string, int>();

            // Create a dict for parties vs rounds won
            for (int i = 0; i < parties.Count; i++)
            {
                roundsWon.Add(parties.ElementAt(i).Key, 0);
            }

            // Process the D'Hondt method
            for (int i = 0; i < rounds; i++)
            {
                // Sort the dictionary
                parties = DictSort(parties);
                // Increment the winner by one.
                roundsWon[parties.ElementAt(parties.Count-1).Key]++;
                // Half the winner
                parties[parties.ElementAt(parties.Count - 1).Key] /= 2;
            }

            return roundsWon;
        }

        private static Dictionary<string, int> DictSort(Dictionary<string, int> toSort)
        {
            // Sort the dictionary to be returned
            var ordered = toSort.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }
    }
}
