using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CMP1903M
{
    class Dhondt
    {
        public string Constituency;
        public int Rounds;
        public int TotalVotes;
        public List<Party> Parties = new();

        public void ImportDataSet(string path)
        {
            // Takes the dataset
            string[] dataSet = File.ReadAllLines(path);

            // Verifies if the dataset is valid
            if (dataSet.Length < 4)
            {
                Console.WriteLine("Invalid dataset provided.");
                return;
            }

            Constituency = dataSet[0].TrimStart('#');
            Rounds = int.Parse(dataSet[1]);
            TotalVotes = int.Parse(dataSet[2]);

            for (var i = 3; i < dataSet.Length; i++)
            {
                List<string> partyData = dataSet[i]
                    .TrimEnd(';')
                    .Split(",")
                    .ToList();
                
                Parties.Add(new Party(partyData[0],
                    int.Parse(partyData[1]),
                    partyData.GetRange(2, partyData.Count - 2)));
            }
        }

        private static Dictionary<string, int> _DictSort(Dictionary<string, int> toSort)
        {
            // Sort the dictionary to be returned
            var ordered = toSort.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
            return ordered;
        }

        public List<List<string>> Calculate(Dictionary<string, int> parties, int rounds)
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
                parties = _DictSort(parties);
                // Increment the winner by one.
                roundsWon[parties.ElementAt(parties.Count - 1).Key]++;
                // Half the winner
                parties[parties.ElementAt(parties.Count - 1).Key] /= 2;
            }

            return GenerateReturn(parties, Parties);
        }

        // Returns a list, index 0 of the list is the party name
        private List<List<string>> GenerateReturn(Dictionary<string, int> parties, List<Party> assignemntData)
        {
            List<List<string>> toReturn = new();

            //Add the names to the output list
            for (int x = 0; x < parties.Length; x++)
            {
                toReturn.Add(List<string>);
                toReturn[x].Add(assignemntData[x].Name)
            }

            // Add constituencies to output list

            for (int y = 0; y < parties.Length; y++)
            {
                for (int i = 0; i < parties[y]; i++)
                {
                    toReturn[y].Add(assignemntData[y].Members[i])
                }
            }

            return toReturn
        }
    }
}
