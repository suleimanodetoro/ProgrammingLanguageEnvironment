using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to draw an equilateral triangle on the canvas.
    /// </summary>
    public class TriangleCommand : Command
    {
        private string sideLengthParameter;

        public TriangleCommand(string sideLengthParameter)
        {
            this.sideLengthParameter = sideLengthParameter;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Assuming that the side length is defined as a variable within the execution context
            int sideLength = context.GetVariableValue(sideLengthParameter);

            if (sideLength < 0)
            {
                throw new InvalidParameterException("Triangle side length must be non-negative.");
            }

            // Rest of your triangle drawing logic
            double height = (Math.Sqrt(3) / 2) * sideLength;
            Point[] triangleVertices = {
        context.CurrentPosition,
        new Point(context.CurrentPosition.X + sideLength, context.CurrentPosition.Y),
        new Point(context.CurrentPosition.X + sideLength / 2, context.CurrentPosition.Y - (int)height)
    };

            renderer.DrawEquilateralTriangle(triangleVertices, context.CurrentColor, context.FillShapes);

            // Update the current position to the last vertex of the triangle
            Point newPoint = triangleVertices[1];
            context.CurrentPosition = newPoint;
            // With newpoint, render pointer location
            renderer.DrawPointer(newPoint);
        }
    }

}
