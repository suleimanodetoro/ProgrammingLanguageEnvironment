﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CommandFactory
    {
        public Command CreateCommand(string commandLine)
        {
            // Split the command string into command name and arguments.
            var parts = commandLine.Split(' ');
            var commandName = parts[0].ToLower();
            var args = parts.Skip(1).ToArray();

            switch (commandName)
            {
                case "moveto":
                    //if moveto command receive a single parameter (moveto 10) meaning it does not contain ',' throw an error. It could have have ',' but not have a second paramter too ( move to 100, )
                    if (args.Length < 1 || !args[0].Contains(','))
                        throw new InvalidParameterException($"Invalid parameters. Expected format: x,y, received: {String.Join(" ", args)}.");
                    var moveToArgs = args[0].Split(',');
                    if (moveToArgs.Length != 2)
                        throw new InvalidParameterException($"Invalid number of parameters for 'moveto'. Expected 2 but received {moveToArgs.Length}.");
                    //If it has two item seperated with ',' but the items given are invalid, throw error
                    if (!int.TryParse(moveToArgs[0], out int x) || !int.TryParse(moveToArgs[1], out int y))
                        throw new InvalidParameterException($"Invalid coordinates provided for 'moveto'. Expected two numbers but received: {args[0]}.");
                    return new MoveToCommand(new Point(x, y));


                case "drawto":
                    // If 'drawto' command receives a single parameter (drawto 10) or does not contain ',' throw an error.
                    if (args.Length < 1 || !args[0].Contains(','))
                        throw new InvalidParameterException($"Invalid parameters for 'drawto'. Expected format: x,y but received: {String.Join(" ", args)}.");
                    var drawToArgs = args[0].Split(',');
                    // If two items separated by ',' are provided but are invalid, throw error.
                    if (!int.TryParse(drawToArgs[0], out int endX) || !int.TryParse(drawToArgs[1], out int endY))
                        throw new InvalidParameterException($"Invalid coordinates provided for 'drawto'. Expected numbers but received: {args[0]}.");
                    return new DrawToCommand(new Point(endX, endY));


                case "pen":
                    // Check if the pen command has received a valid color.
                    if (args.Length < 1 || String.IsNullOrEmpty(args[0]))
                        throw new InvalidParameterException("Invalid color provided for 'pen' command.");
                    return new PenCommand(args[0]);


                case "rect":
                    // If 'rect' command receives a single parameter (rect 10,20) or does not contain ',' throw an error.
                    if (args.Length < 1 || !args[0].Contains(','))
                        throw new InvalidParameterException($"Invalid parameters for 'rect'. Expected format: width,height but received: {String.Join(" ", args)}.");
                    var rectArgs = args[0].Split(',');
                    // If two items separated by ',' are provided but are invalid, throw error.
                    if (!int.TryParse(rectArgs[0], out int rectWidth) || !int.TryParse(rectArgs[1], out int rectHeight))
                        throw new InvalidParameterException($"Invalid dimensions provided for 'rect'. Expected numbers but received: {args[0]}.");
                    return new RectangleCommand(rectWidth, rectHeight);


                case "circle":
                    // Check if circle command receives a valid radius.
                    if (args.Length < 1 || !int.TryParse(args[0], out int circleRadius))
                        throw new InvalidParameterException($"Invalid radius provided for 'circle'. Expected number but received: {args[0]}.");
                    return new CircleCommand(circleRadius);


                case "tri":
                    // Check if triangle command receives a valid side length.
                    if (args.Length < 1 || !int.TryParse(args[0], out int triangleSide))
                        throw new InvalidParameterException($"Invalid side length provided for 'tri'. Expected number but received: {args[0]}.");
                    return new TriangleCommand(triangleSide);


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
        }
    }
}