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
            if (radius <= 0)
                throw new InvalidParameterException($"Invalid radius for 'circle'. Radius must be positive. Received: {radius}");

            this.radius = radius;
        }

        public int Radius => radius;

        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawCircle(radius);
        }
    }

}
