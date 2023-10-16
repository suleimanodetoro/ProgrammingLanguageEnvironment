using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CommandParser
    {
        public List<ICommand> ParseCommands(string commandText)
        {
            List<ICommand> commands = new List<ICommand>();
            string[] lines = commandText.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string line in lines)
            {
                string[] parts = line.Split(' ');
                string commandName = parts[0].ToLower();

                switch (commandName)
                {
                    case "moveto":
                        string[] moveArgs = parts[1].Split(',');
                        int x = int.Parse(moveArgs[0].Trim());
                        int y = int.Parse(moveArgs[1].Trim());
                        //implement moveto command class
                        commands.Add(new MoveToCommand(x, y));
                        break;
                    case "circle":
                        int radius = int.Parse(parts[1]);
                        //implement circle command class
                        commands.Add(new CircleCommand(radius));
                        break;
                    case "pen":
                        string color = parts[1].ToLower();
                        commands.Add(new PenCommand(color));
                        break;
                    default:
                        //handle errors later haha
                        break;
                }
            }
            return commands;
        }
    }
}
