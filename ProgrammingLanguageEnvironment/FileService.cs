using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Defines the interface for a service that handles file operations for commands.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves the specified commands to a file.
        /// </summary>
        /// <param name="filePath">The file path where commands will be saved.</param>
        /// <param name="commands">The commands to be saved to the file.</param>
        void SaveCommandsToFile(string filePath, IEnumerable<string> commands);

        /// <summary>
        /// Loads commands from a specified file.
        /// </summary>
        /// <param name="filePath">The file path from which commands will be loaded.</param>
        /// <returns>An enumerable collection of strings representing the loaded commands.</returns>
        IEnumerable<string> LoadCommandsFromFile(string filePath);


    }

    /// <summary>
    /// Class for providing services for saving to and loading from command files.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Saves a collection of commands to the specified file.
        /// </summary>
        /// <param name="filePath">The path to the file where the commands will be saved.</param>
        /// <param name="commands">An enumerable collection of command strings to save.</param>
        public void SaveCommandsToFile(string filePath, IEnumerable<string> commands)
        {
            // Write all lines to the file
            File.WriteAllLines(filePath, commands);
        }

        /// <summary>
        /// Loads and returns a collection of commands from the specified file.
        /// </summary>
        /// <param name="filePath">The path to the file from which the commands will be loaded.</param>
        /// <returns>An enumerable collection of strings representing the loaded commands.</returns>
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
