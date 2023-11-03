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
        {
            if (sideLength<= 0)
                throw new InvalidParameterException("Side length cannot be negative or zero.");
            this.sideLength = sideLength;
        }
        public int SideLength => sideLength; // Public getter for the side length - aid for testingggg

        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawEquilateralTriangle(sideLength);
        }
    }

}
