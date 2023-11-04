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
        bool CheckSyntax(string commands, out string errorMessage);
        void DisplayMessage(string message); // Declare the method in the interface
        void ClearCanvas(); // to help clear the canvas on checks

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
        public void ClearCanvas()
        {
            // Delegate to the canvas renderer's method to clear the canvas
            _canvasRenderer.ClearCanvas();
        }

        public void ExecuteCommands(string commands)
            {
                var parsedCommands = _commandParser.ParseCommands(commands);
                _canvasRenderer.ExecuteCommands(parsedCommands);
            }

            public bool CheckSyntax(string commands, out string? errorMessage)
            {
            errorMessage = null;
            try
            {
                _commandParser.ParseCommands(commands);
                return true; // Syntax is correct
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message; // Capture the exception message
                return false; // Syntax is incorrect
            }
        }

            public void DisplayMessage(string message)
            {
            
                // Delegate to the canvas renderer's method to display the message
                _canvasRenderer.DisplayTextOnCanvas(message);
            }
        }
    }


