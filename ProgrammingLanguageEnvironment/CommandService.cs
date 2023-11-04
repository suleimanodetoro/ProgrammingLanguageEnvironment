using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public interface ICommandService
    {
        void ExecuteCommands(string commands);
        void CheckSyntax(string commands);
    }

    public class CommandService : ICommandService
    {
        private readonly CommandParser _commandParser;
        private readonly CanvasRenderer _canvasRenderer;

        public CommandService(CommandParser commandParser, CanvasRenderer canvasRenderer)
        {
            _commandParser = commandParser;
            _canvasRenderer = canvasRenderer;
        }

        public void ExecuteCommands(string commands)
        {
            var parsedCommands = _commandParser.ParseCommands(commands);
            _canvasRenderer.ExecuteCommands(parsedCommands);
        }

        public void CheckSyntax(string commands)
        {
            // This method could be expanded to provide more detailed syntax checking
            var parsedCommands = _commandParser.ParseCommands(commands);
            // If no exception is thrown, syntax is assumed to be okay
        }
    }
}
