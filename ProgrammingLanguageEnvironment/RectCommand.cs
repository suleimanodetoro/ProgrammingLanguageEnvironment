using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class RectangleCommand : Command
    {
        private int width;
        private int height;

        public RectangleCommand(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        public int Width => width;
        public int Height => height;

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.DrawRectangle(width, height);
        }
    }

}
