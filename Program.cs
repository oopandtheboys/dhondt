using System;

namespace CMP1903M
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instance.
            var dhondtMethod = new Dhondt(@"..\..\..\sample_sets\Assessment1Data.txt");
            
            // Calculate D'Hondt using the above dataset file imported through the constructor.
            var calculateMethod = dhondtMethod.Calculate();

            // Print the results of a D'Hondt calculation to console.
            dhondtMethod.PrintResults();

            // Import a result set from file.
            var existingResultSet = Dhondt.ImportResultSetFromFile(@"..\..\..\sample_sets\Assessment1TestResults.txt");
        }
    }
}