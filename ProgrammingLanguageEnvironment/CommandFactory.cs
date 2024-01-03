using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// A factory for creating Command objects based on textual command lines.
    /// </summary>
    public class CommandFactory
    {
        /// <summary>
        /// Creates a Command object based on a given command line.
        /// </summary>
        /// <param name="commandLine">The text representation of the command.</param>
        /// <returns>A Command object corresponding to the given command line.</returns>
        /// <exception cref="InvalidParameterException">Thrown when the command has invalid parameters.</exception>
        /// <exception cref="InvalidCommandException">Thrown when the command is unknown.</exception>
        public Command CreateCommand(string commandLine)
        {
            // Split the command string into command name and arguments.
            var parts = commandLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            // Inside the CreateCommand method
            if (parts.Length >= 3 && parts[1] == "=")
            {
                string variableName = parts[0];
                string expression = string.Join(" ", parts.Skip(2));
                return new AssignmentCommand(variableName, expression);
            }
            var commandName = parts[0].ToLower();
            var args = parts.Skip(1).ToArray();

            // Process the command line based on the command name.
            switch (commandName)
            {
                case "var":
                    // Check if the format is 'var x = 100'
                    if (args.Length != 3 || args[1] != "=")
                        throw new InvalidParameterException($"Invalid format for 'var' command. Expected 'var name = value' but received: {String.Join(" ", args)}.");
                    string varName = args[0];
                    int initialValue;
                    if (!int.TryParse(args[2], out initialValue))
                        throw new InvalidParameterException($"Invalid value for 'var' command. Expected an integer but received: {args[2]}.");
                    return new VariableCommand(varName, initialValue);
                case "while":
                    // handled in parser
                    break;
                case "if":
                    // 'if' command is handled in the parser
                    break;

                case "moveto":
                    
                    var moveToArgs = args[0].Split(',');
                    // moveToArgs now represent either variable names or direct coordinates
                    return new MoveToCommand(moveToArgs[0], moveToArgs[1]);



                case "drawto":
                    var drawToArgs = args[0].Split(',');
                    return new DrawToCommand(drawToArgs[0], drawToArgs[1]);


                case "pen":
                    // Check if the pen command has received a valid color.
                    if (args.Length < 1 || String.IsNullOrEmpty(args[0]))
                        throw new InvalidParameterException("Invalid color provided for 'pen' command.");
                    return new PenCommand(args[0]);
                case "colour":
                    if (args.Length != 1 || !args[0].Contains(','))
                        throw new InvalidParameterException("Invalid parameters for 'colour'. Expected format: R,G,B.");

                    var rgbArgs = args[0].Split(',');
                    if (rgbArgs.Length != 3)
                        throw new InvalidParameterException("Invalid number of parameters for 'colour'. Expected 3 RGB values.");

                    if (!int.TryParse(rgbArgs[0], out int r) || !int.TryParse(rgbArgs[1], out int g) || !int.TryParse(rgbArgs[2], out int b))
                        throw new InvalidParameterException("Invalid RGB values provided for 'colour'.");

                    return new ColourCommand(Color.FromArgb(r, g, b));

                case "rect":
                    var rectArgs = args[0].Split(',');
                    return new RectangleCommand(rectArgs[0], rectArgs[1]);

                case "tri":
                    // Instead of parsing the side length here, pass the string to the command
                    return new TriangleCommand(args[0]);


                case "circle":
                    return new CircleCommand(args[0]);





                case "fill":
                    if (args.Length < 1 || (args[0] != "on" && args[0] != "off"))
                        throw new InvalidParameterException($"Invalid parameter for 'fill'. Expected 'on' or 'off' but received: {String.Join(" ", args)}.");

                    return new FillCommand(args[0] == "on");
                case "reset":
                    // The 'reset' command should not have any parameters.
                    if (args.Length > 0)
                        throw new InvalidParameterException($"The 'reset' command does not expect any parameters but received: {String.Join(" ", args)}.");
                    return new ResetCommand();


                case "clear":
                    // The 'clear' command should not have any parameters.
                    if (args.Length > 0)
                        throw new InvalidParameterException($"The 'clear' command does not expect any parameters but received: {String.Join(" ", args)}.");
                    return new ClearCommand();


                default:
                    throw new InvalidCommandException($"Unknown command \"{commandName}\".");
            }
            // This line should never be reached due to the above default case
            throw new InvalidOperationException("Unhandled command type.");
        }
    }
}
