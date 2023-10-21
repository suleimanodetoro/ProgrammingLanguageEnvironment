using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class TriangleCommand : ICommand
    {
        public int SideLength { get; }

        public TriangleCommand(int side)
        {
            SideLength = side;
        }
        public void Execute(CanvasRenderer renderer)
        {
            renderer.DrawTriangle(SideLength);
        }
    }
}
