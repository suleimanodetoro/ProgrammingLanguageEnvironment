using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class PenCommand: Command
    {
        private Color penColor;

        public PenCommand(string colorName)
        {
            penColor = Color.FromName(colorName);
        }

        public Color PenColor => penColor;

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.SetPenColor(penColor);
        }


    }
}
