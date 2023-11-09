using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Defines the interface for a service that executes and manages drawing commands.
    /// </summary>
    public interface ICommandService
    {
        /// <summary>
        /// Executes a series of commands on a canvas.
        /// </summary>
        /// <param name="commands">The string containing the commands to be executed.</param>
        void ExecuteCommands(string commands);
        /// <summary>
        /// Checks the syntax of the commands without executing them.
        /// </summary>
        /// <param name="commands">The string containing the commands to be checked.</param>
        /// <param name="errorMessage">Outputs an error message if the syntax is incorrect.</param>
        /// <returns>True if the syntax is correct; otherwise, false.</returns>
        bool CheckSyntax(string commands, out string errorMessage);

        /// <summary>
        /// Displays a message on the canvas.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>
        void DisplayMessage(string message); 

        /// <summary>
        /// Clears the canvas.
        /// </summary>
        void ClearCanvas(); // to help clear the canvas on checks

    }

    /// <summary>
    /// Provides services for executing and managing drawing commands.
    /// </summary>
    public class CommandService : ICommandService
        {
        private readonly CommandParser _commandParser;
        private readonly ICanvasRenderer _canvasRenderer;


        /// <summary>
        /// Initializes a new instance of the <see cref="CommandService"/> class with the specified command parser and canvas renderer.
        /// </summary>
        /// <param name="commandParser">The parser used to parse the commands.</param>
        /// <param name="canvasRenderer">The renderer used to draw on the canvas.</param>
        public CommandService(CommandParser commandParser, ICanvasRenderer canvasRenderer)
        {
          _commandParser = commandParser;
          _canvasRenderer = canvasRenderer;
        }
        /// <summary>
        /// Clears the drawing canvas.
        /// </summary>
        public void ClearCanvas()
        {
            // Delegate to the canvas renderer's method to clear the canvas
            _canvasRenderer.ClearCanvas();
        }

        /// <summary>
        /// executes a series of drawing commands.
        /// </summary>
        /// <param name="commands">The string containing the commands to be executed.</param>
        public void ExecuteCommands(string commands)
        {
                var parsedCommands = _commandParser.ParseCommands(commands);
                _canvasRenderer.ExecuteCommands(parsedCommands);
        }

        /// <summary>
        /// Checks the syntax of a series of drawing commands.
        /// </summary>
        /// <param name="commands">The string containing the commands to be checked.</param>
        /// <param name="errorMessage">When this method returns, contains the error message if the syntax is incorrect; otherwise, null.</param>
        /// <returns>True if the syntax is correct; otherwise, false.</returns>
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

        /// <summary>
        /// Displays a text message on the canvas.
        /// </summary>
        /// <param name="message">The message to be displayed.</param>

        public void DisplayMessage(string message)
            {
            
                // Delegate to the canvas renderer's method to display the message
                _canvasRenderer.DisplayTextOnCanvas(message);
            }
        }
    }


