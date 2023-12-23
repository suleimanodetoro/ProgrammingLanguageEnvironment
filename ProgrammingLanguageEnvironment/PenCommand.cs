using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{

    public class PenCommand : Command
    {

        private Color penColor;
        private static readonly HashSet<string> ValidColors = new HashSet<string>
    {
        "red", "blue", "green", "black"
    };


        public PenCommand(string colorName)
        {
            if (!ValidColors.Contains(colorName.ToLower()))
            {
                throw new InvalidParameterException($"Invalid color provided for 'pen' command. Supported colors are: {string.Join(", ", ValidColors)}.");
            }
            penColor = Color.FromName(colorName);
        }


        public Color PenColor => penColor;


        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            context.CurrentColor = penColor;
        }



    }
}
