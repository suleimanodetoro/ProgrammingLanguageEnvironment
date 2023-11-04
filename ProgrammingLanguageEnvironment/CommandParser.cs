using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CommandParser
    {
        private CommandFactory commandFactory;

        public CommandParser()
        {
            commandFactory = new CommandFactory();
        }

        public List<Command> ParseCommands(string rawInput)
        {
            // Check if the raw input is null, empty, or consists only of white-space characters
            if (string.IsNullOrWhiteSpace(rawInput))
            {
                throw new ArgumentException("Input cannot be empty or whitespace.");
            }

            var commands = new List<Command>();

            // Split the input into individual lines/commands
            var commandLines = rawInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandLine in commandLines)
            {
                // Assuming CreateCommand handles its own validation and may throw exceptions as needed
                var command = commandFactory.CreateCommand(commandLine);
                commands.Add(command); // Add the command to the list
            }

            return commands;
        }
    }
}