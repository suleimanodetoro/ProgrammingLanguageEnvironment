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
            if (string.IsNullOrWhiteSpace(rawInput))
                throw new ArgumentException("Input cannot be empty or whitespace.");

            var commands = new List<Command>();
            var loopCommands = new List<Command>();
            bool isInsideLoop = false;
            string loopCondition = "";



            var lines = rawInput.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var line in lines)
            {
                string trimmedLine = line.Trim();
                if (trimmedLine.StartsWith("while"))
                {
                    isInsideLoop = true;
                    loopCondition = trimmedLine.Substring("while".Length).Trim();
                    loopCommands.Clear();
                }
                else if (trimmedLine.StartsWith("endloop") && isInsideLoop)
                {
                    isInsideLoop = false;
                    commands.Add(new WhileCommand(loopCondition, new List<Command>(loopCommands)));
                    loopCommands.Clear();
                }
                else
                {
                    var command = commandFactory.CreateCommand(line);
                    if (isInsideLoop)
                        loopCommands.Add(command);
                    else
                        commands.Add(command);
                }
            }

            if (isInsideLoop)
                throw new InvalidOperationException("Unclosed loop found in script.");

            return commands;
        }
    }
}