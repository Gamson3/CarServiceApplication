using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRPJHF
{
    // Class responsible for loading works from a file
    public class Loader
    {
        private readonly Parser parser;
        private readonly List<Work> works;

        public Loader()
        {
            parser = new Parser();
            works = new List<Work>();
        }

        public List<Work> LoadFile(string filePath)
        {
            try
            {
                // Clear any previously loaded works
                works.Clear();

                // Read all lines from the file
                string[] lines = File.ReadAllLines(filePath);

                // Parse each line and create Work instances
                foreach (string line in lines)
                {
                    // Split the line into columns
                    string[] columns = line.Split(';');

                    // Use the parser to create a Work instance from the line
                    Work work = parser.Parse(columns);

                    if (work != null)
                    {
                        works.Add(work);
                    }
                    else
                    {
                        // Handle the case where parsing of the line fails
                        Console.WriteLine($"Invalid line format: {line}");
                    }
                }

                // Prompt the user if no valid works were loaded or if works were loaded in the wrong format
                if (works.Count == 0)
                {
                    Console.WriteLine("No valid works were loaded from the file, or the file contains data in the wrong format.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading file: {ex.Message}");
            }

            return works;
        }


    }
}
