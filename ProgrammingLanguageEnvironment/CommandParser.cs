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
            var commands = new List<Command>();

            // Split the input into individual lines/commands
            var commandLines = rawInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var commandLine in commandLines)
            {
                var command = commandFactory.CreateCommand(commandLine);
                commands.Add(command); // Directly add the command to the list without further checks.
            }

            return commands;
        }
    }
}
