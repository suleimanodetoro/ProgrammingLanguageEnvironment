using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
/*        this is where it started
*/
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
            var ifCommandStack = new Stack<KeyValuePair<string, List<Command>>>();
            var methodCommandsStack = new Stack<MethodData>();
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
                else if (trimmedLine.StartsWith("if"))
                {
                    var condition = trimmedLine.Substring(2).Trim();
                    ifCommandStack.Push(new KeyValuePair<string, List<Command>>(condition, new List<Command>()));
                }
                else if (trimmedLine == "endif" && ifCommandStack.Count > 0)
                {
                    var ifData = ifCommandStack.Pop();
                    if (ifCommandStack.Count > 0)
                    {
                        ifCommandStack.Peek().Value.Add(new IfCommand(ifData.Key, ifData.Value));
                    }
                    else
                    {
                        commands.Add(new IfCommand(ifData.Key, ifData.Value));
                    }
                }
                else if (trimmedLine.StartsWith("method"))
                {
                    // Handling method definition
                    var methodParts = trimmedLine.Substring("method".Length).Trim().Split(new[] { ' ' }, 2);
                    string methodName = methodParts[0].Trim();
                    var parameters = methodParts.Length > 1 ? methodParts[1].Trim('(', ')').Split(',').Select(p => p.Trim()).ToList() : new List<string>();

                    // Push a new MethodData to the stack
                    methodCommandsStack.Push(new MethodData(methodName, parameters));
                }
                else if (trimmedLine.StartsWith("endmethod") && methodCommandsStack.Count > 0)
                {
                    // Pop the MethodData and add it to the commands list
                    var methodData = methodCommandsStack.Pop();
                    var methodCommand = new MethodCommand(methodData.MethodName, methodData.Parameters, methodData.Commands);

                    // Add MethodCommand to the main commands list for execution
                    commands.Add(methodCommand);

                    // Register the method immediately in the execution context (optional based on your design)
                    //context.AddMethod(methodCommand);
                }

                else if (trimmedLine.Contains("(") && trimmedLine.Contains(")"))
                {
                    var methodCallParts = trimmedLine.Split(new char[] { '(', ')' }, StringSplitOptions.RemoveEmptyEntries);
                    string methodName = methodCallParts[0].Trim();
                    var actualParameters = methodCallParts[1].Split(',').Select(p => p.Trim()).ToList();
                    commands.Add(new MethodCallCommand(methodName, actualParameters));
                }
                else
                {
                    var command = commandFactory.CreateCommand(line);
                    if (isInsideLoop)
                        loopCommands.Add(command);
                    else if (ifCommandStack.Count > 0)
                        ifCommandStack.Peek().Value.Add(command);
                    else if (methodCommandsStack.Count > 0)
                        methodCommandsStack.Peek().Commands.Add(command);  // Use Commands property of MethodData
                    else
                        commands.Add(command);
                }
            }

            if (isInsideLoop)
                throw new InvalidOperationException("Unclosed loop found in script.");
            if (ifCommandStack.Count > 0)
                throw new InvalidOperationException("Unclosed if block found in script.");
            if (methodCommandsStack.Count > 0)
                throw new InvalidOperationException("Unclosed method block found in script.");


            return commands;
        }
    }
}