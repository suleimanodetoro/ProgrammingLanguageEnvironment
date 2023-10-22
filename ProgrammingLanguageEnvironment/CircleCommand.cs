using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CircleCommand : Command
    {
        private int radius;

        public CircleCommand(int radius)
        {
            this.radius = radius;
        }

        public int Radius => radius;

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.DrawCircle(radius);
        }
    }

}
