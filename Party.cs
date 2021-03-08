using System.Collections.Generic;

namespace CMP1903M
{
    public struct Party
    {
        public string Name;
        public int Votes;
        public List<string> Members;

        public Party(string name, int votes, List<string> members)
        {
            Name = name;
            Votes = votes;
            Members = members;
        }
    }
}