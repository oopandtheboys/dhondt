using System.Collections.Generic;

namespace CMP1903M
{
    public class Party
    {
        public string Name;
        public int Votes;
        public List<string> Members;
        public int Seats;

        public Party(string name, int votes, List<string> members)
        {
            Name = name;
            Votes = votes;
            Members = members;
            Seats = 0;
        }
    }
}