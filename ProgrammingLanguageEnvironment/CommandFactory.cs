using System;
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
                    var moveToArgs = args[0].Split(',');
                    Point moveToTarget = new Point(int.Parse(moveToArgs[0]), int.Parse(moveToArgs[1]));
                    return new MoveToCommand(moveToTarget);
                case "drawto":
                    var drawToArgs = args[0].Split(',');
                    Point drawToEnd = new Point(int.Parse(drawToArgs[0]), int.Parse(drawToArgs[1]));
                    return new DrawToCommand(drawToEnd);
                case "pen":
                    return new PenCommand(args[0]);
                case "rect":
                    var rectArgs = args[0].Split(',');
                    int width = int.Parse(rectArgs[0]);
                    int height = int.Parse(rectArgs[1]);
                    return new RectangleCommand(width, height);
                case "circle":
                    int radius = int.Parse(args[0]);
                    return new CircleCommand(radius);
                case "tri":
                    int sideLength = int.Parse(args[0]);
                    return new TriangleCommand(sideLength);
                case "fill":
                    return new FillCommand(args[0] == "on");
                case "reset":
                    return new ResetCommand();
                case "clear":
                    return new ClearCommand();
                default:
                    throw new InvalidCommandException($"Unknown command \"{commandName}\".");
            }
        }
    }
}
