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
            this.endPoint = endPoint;
        }

        public Point EndPoint => endPoint;

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.DrawLine(endPoint);
        }
    }
}
