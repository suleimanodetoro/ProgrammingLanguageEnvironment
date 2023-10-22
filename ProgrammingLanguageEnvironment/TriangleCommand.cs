using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class TriangleCommand : Command
    {
        private readonly int sideLength;

        public TriangleCommand(int sideLength)
        {this.sideLength = sideLength;
        }

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.DrawEquilateralTriangle(sideLength);
        }
    }

}
