using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class DrawToCommand : ICommand
    {
        public Point EndPoint { get; }

        public DrawToCommand(int x, int y)
        {
            EndPoint = new Point(x, y);
        }

        public void Execute(CanvasRenderer renderer)
        {
            renderer.DrawLine(EndPoint);
        }
    }
}
