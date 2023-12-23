using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CommandService : ICommandService
    {
        private readonly CommandParser _commandParser;
        private readonly ICanvasRenderer _canvasRenderer;

        public CommandService(CommandParser commandParser, ICanvasRenderer canvasRenderer)
        {
            _commandParser = commandParser;
            _canvasRenderer = canvasRenderer;
        }

        public void ExecuteCommands(string commands)
        {
            ExecutionContext context = new ExecutionContext();
            var parsedCommands = _commandParser.ParseCommands(commands);
            foreach (var command in parsedCommands)
            {
                command.Execute(_canvasRenderer, context);
            }
        }


        // New method for executing multiple command sets in parallel
        public async Task ExecuteCommandsParallel(IEnumerable<string> commandSets)
        {
            var tasks = commandSets.Select(commands =>
            {
                return Task.Run(() =>
                {
                    ExecuteCommands(commands);
                });
            });

            await Task.WhenAll(tasks);
        }

        public bool CheckSyntax(string commands, out string errorMessage)
        {
            errorMessage = null;
            try
            {
                _commandParser.ParseCommands(commands);
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                return false;
            }
        }

        public void DisplayMessage(string message)
        {
            _canvasRenderer.DisplayTextOnCanvas(message);
        }

        public void ClearCanvas()
        {
            _canvasRenderer.ClearCanvas();
        }
    }
}
