using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class DrawToCommand : Command
    {
        private Point endPoint;

        public DrawToCommand(Point endPoint)
        {
            // Check if the provided Point is valid. For example, we can check if X and Y are non-negative.
            if (endPoint.X < 0 || endPoint.Y < 0)
            {
                throw new InvalidParameterException("Coordinates cannot be negative. Error in class file");
            }
            this.endPoint = endPoint;
        }

        public Point EndPoint => endPoint;

        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawLine(endPoint);
        }
    }
}
