using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TRPJHF
{
    // Class responsible for parsing lines into Work objects
    public class Parser
    {
        // Method to parse a line and create a Work object
        public Work Parse(string[] columns)
        {
            if (columns.Length < 3)
            {
                throw new ArgumentException("Invalid number of columns in input data.");
            }

            if (columns.Length == 3)
            {
                string name = columns[0];
                int executionTime, materialCost;

                // Check if parsing of execution time and material cost succeeds
                if (int.TryParse(columns[1], out executionTime) && int.TryParse(columns[2], out materialCost))
                {
                    return new Work(name, executionTime, materialCost);
                }
                else
                {
                    // Handle invalid numeric format
                    Console.WriteLine($"Invalid numeric format in line: {string.Join(";", columns)}");
                    return null;
                }
            }
            else
            {
                // Handle invalid number of columns
                Console.WriteLine($"Invalid number of columns in line: {string.Join(";", columns)}");
                return null;
            }
        }

    }
}

