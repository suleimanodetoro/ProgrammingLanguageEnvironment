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

        public TriangleCommand(string[] parameters)
        {
            if (parameters.Length != 1 || !int.TryParse(parameters[0], out sideLength))
            {
                throw new ArgumentException("Invalid parameters for Triangle Command. Only takes one para");
            }
        }

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.DrawEquilateralTriangle(sideLength);
        }
    }

}
