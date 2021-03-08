using System;
using System.Collections.Generic;
using System.Linq;

namespace CMP1903M
{
    class Program
    {
        static void Main(string[] args)
        {
            // Proof of Concept
            var exampleObject = new Dhondt();
            exampleObject.ImportDataSet("Assessment1Data.txt");
            Console.WriteLine($"{exampleObject.TotalVotes} {exampleObject.Constituency} {exampleObject.Rounds}");

            foreach (var party in exampleObject.Parties)
            {
                Console.WriteLine(party.Name);
                Console.WriteLine(party.Votes);
            }
        }
    }
}