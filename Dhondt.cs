using System;
using System.Collections.Generic;

namespace CMP1903M
{
    class Dhondt
    {
        private string _FilePath;
        public string Constituency { get; private set; }
        public int RoundCount { get; private set; }
        public int TotalVotes { get; private set; }
        public Dictionary<string, int> PartyVotes { get; private set; } = new Dictionary<string, int>();
        public Dictionary<string, List<string>> PartyMembers { get; private set; } = new Dictionary<string, List<string>>();

        public Dhondt(string FilePath)
        {
            _FilePath = FilePath;
            _ReadDataset();
        }

        private void _ReadDataset()
        {
            string[] dataset = System.IO.File.ReadAllLines(_FilePath);

            if (dataset.Length < 4)
                Console.WriteLine("Invalid dataset provided.");

            Constituency = dataset[0].Substring(1);
            RoundCount = Int32.Parse(dataset[1]);
            TotalVotes = Int32.Parse(dataset[2]);

            for (var i = 3; i < dataset.Length; i++)
            {
                string[] party = dataset[i].Split(",");
                PartyVotes.Add(party[0], Int32.Parse(party[1]));
            }
        }
    }
}
