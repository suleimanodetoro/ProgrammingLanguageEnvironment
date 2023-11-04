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

    }

    public class FileService : IFileService
    {
        // Method to save commands to a text file
        public void SaveCommandsToFile(string filePath, IEnumerable<string> commands)
        {
            // Write all lines to the file
            File.WriteAllLines(filePath, commands);
        }
    }
}
