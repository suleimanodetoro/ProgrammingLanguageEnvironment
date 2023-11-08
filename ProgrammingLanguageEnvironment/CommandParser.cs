using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// This code parses raw input text into a list of Command objects that can be executed.
    /// </summary>
    public class CommandParser
    {
        // The factory responsible for creating command objects from strings.
        private CommandFactory commandFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        public CommandParser()
        {
            commandFactory = new CommandFactory();
        }

        /// <summary>
        /// Parses a string containing raw input commands into a list of executable Command objects.
        /// </summary>
        /// <param name="rawInput">The raw text input containing commands.</param>
        /// <returns>A list of Command objects ready for execution.</returns>
        /// <exception cref="ArgumentException">Thrown when the input is null, empty, or whitespace.</exception>
        public List<Command> ParseCommands(string rawInput)
        {
            // Check if the raw input is null, empty, or consists only of white-space characters
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                throw new ArgumentException("Input cannot be empty or whitespace.");
            }
            // Initialize the list to store the parsed Command objects.
            var commands = new List<Command>();

            // Split the input into individual lines/commands
            // and removing any empty entries.
            var commandLines = rawInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            // Iterate over each command line extracted from the raw input.
            foreach (var commandLine in commandLines)
            {
                // Use the commandFactory to create a Command object from the command line.
                // The factory is responsible for parsing the command line and throwing exceptions if necessary.
                var command = commandFactory.CreateCommand(commandLine);

                // Add the newly created Command object to the list of commands.
                commands.Add(command); // Add the command to the list
            }
            Console.WriteLine("Command"+commands);

            return commands;
        }
    }
}