using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class ColourCommand : Command
    {
        private Color color;

        public ColourCommand(Color color)
        {
            this.color = color;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            renderer.SetPenColor(color);
            context.CurrentColor = color;
        }
    }

}
