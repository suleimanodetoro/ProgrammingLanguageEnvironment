using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public interface IFileService
    {
        void SaveCommandsToFile(string filePath, IEnumerable<string> commands);
        IEnumerable<string> LoadCommandsFromFile(string filePath);


    }

    public class FileService : IFileService
    {
        // Method to save commands to a text file
        public void SaveCommandsToFile(string filePath, IEnumerable<string> commands)
        {
            // Write all lines to the file
            File.WriteAllLines(filePath, commands);
        }

        //Load command to input box
        public IEnumerable<string> LoadCommandsFromFile(string filePath)
        {
            try
            {
                // Read all lines from the file
                return File.ReadAllLines(filePath);
            }
            catch (Exception ex)
            {
                // Handle any exceptions that might occur during file read
                throw new ApplicationException("Error loading file: " + ex.Message, ex);
            }
        }
    }
}
