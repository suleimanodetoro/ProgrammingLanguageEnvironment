using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class PenCommand: ICommand
    {
      public Color PenColor { get; }
        public PenCommand (string colorName)
        {
            //Convert string color name to System.Drawing.Color
            PenColor = Color.FromName (colorName);
        }

        public void Execute (CanvasRenderer renderer)
        {
            renderer.SetPenColor (PenColor);
        }


    }
}
