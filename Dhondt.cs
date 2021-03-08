using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace CMP1903M
{
    class Dhondt
    {
        public string Constituency;
        public int Rounds;
        public int TotalVotes;
        public List<Party> Parties = new List<Party>();

        public void ImportDataSet(string path)
        {
            string[] dataSet = System.IO.File.ReadAllLines(path);

            if (dataSet.Length < 4)
            {
                Console.WriteLine("Invalid dataset provided.");
                return;
            }

            Constituency = dataSet[0].TrimStart('#');
            Rounds = Int32.Parse(dataSet[1]);
            TotalVotes = Int32.Parse(dataSet[2]);

            for (var i = 3; i < dataSet.Length; i++)
            {
                List<string> partyData = dataSet[i]
                    .TrimEnd(';')
                    .Split(",")
                    .ToList();
                
                Parties.Add(new Party(partyData[0],
                    Int32.Parse(partyData[1]),
                    partyData.GetRange(2, partyData.Count - 2)));
            }
        }
    }
}
