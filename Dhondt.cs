using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace CMP1903M
{
    class Dhondt
    {
        private int _rounds;
        private int _totalVotes;
        private Dictionary<string, Party> _parties = new Dictionary<string, Party>();
        private string _path;

        // Constructor to enable flexibility if user doesn't wish to import from file.
        public Dhondt(int rounds, int total_votes, Dictionary<string, Party> parties)
        {
            _rounds = rounds;
            _totalVotes = total_votes;
            _parties = parties;
        }

        public Dhondt(string path)
        {
            _path = path;
            _ImportDataSetFromFile(_path);
        }

        // This method handles the import of vote data for D'hondt Method processing.
        private void _ImportDataSetFromFile(string path)
        {
            string[] dataSet = File.ReadAllLines(path);

            // Basic validation check to ensure dataset is using the correct format.
            if (dataSet.Length < 4)
            {
                Console.WriteLine("Invalid dataset provided.");
                return;
            }

            _rounds = int.Parse(dataSet[1]);
            _totalVotes = int.Parse(dataSet[2]);

            // Iterates over the lines containing data for each party, tidies up the strings and adds them to an object within a dictionary.
            for (var i = 3; i < dataSet.Length; i++)
            {
                List<string> partyData = dataSet[i]
                    .TrimEnd(';')
                    .Split(",")
                    .ToList();
                
                _parties.Add(partyData[0], new Party(partyData[0],
                    int.Parse(partyData[1]),
                    partyData.GetRange(2, partyData.Count - 2)));
            }
        }

        // This method handles the import of existing calculated results so the application can be tested for accuracy.
        public static Dictionary<string, List<string>> ImportResultSetFromFile(string path)
        {
            string[] resultSet = File.ReadAllLines(path);

            var parties = new Dictionary<string, List<string>>();

            // Iterates over the lines containing data for each party, tidies up the strings and adds them to an object within a dictionary.
            for (var i = 1; i < resultSet.Length; i++)
            {
                List<string> partyData = resultSet[i]
                    .TrimEnd(';')
                    .Split(",")
                    .ToList();

                parties.Add(partyData[0], partyData.GetRange(1, partyData.Count - 1));
            }

            return parties;
        }

        // This method calculates the D'hondt Method.
        public Dictionary<string, List<string>> Calculate()
        {
            var elected = new Dictionary<string, List<string>>();

            // Iterate for as many rounds as specified through the input.
            for (int i = 0; i < _rounds; i++)
            {
                // Variables used to keep track of the highest quotient value and the respective party the value was generated from.
                int quotient = 0;
                string party_name = "";
                
                foreach (var party in _parties)
                    if (quotient < party.Value.Votes / (i + 1))
                    {
                        quotient = party.Value.Votes / (i + 1);
                        party_name = party.Value.Name;

                    }

                // Once the round winner has been identified, update their new vote count and seats.
                _parties[party_name].Votes = quotient;
                _parties[party_name].Seats++;
            }

            // Assemble a dictionary containing party name and newly elected members to return.
            foreach (var party in _parties)
                if (party.Value.Seats > 0)
                    elected.Add(party.Value.Name, party.Value.Members.GetRange(0, party.Value.Seats));

            return elected;
        }

        // This method is used to print the results of a D'Hondt Method to console.
        public void PrintResults()
        {
            Console.WriteLine("=========================");
            Console.WriteLine($"Total Votes: {_totalVotes}\n");
            foreach (var party in _parties)
                if (party.Value.Seats > 0)
                    Console.WriteLine($"{party.Value.Name} - {String.Join(',', party.Value.Members.GetRange(0, party.Value.Seats))}");
            Console.WriteLine("=========================");
        }
    }
}