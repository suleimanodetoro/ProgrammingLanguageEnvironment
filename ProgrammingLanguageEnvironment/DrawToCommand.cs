using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to draw a line to a specified endpoint on a canvas.
    /// </summary>
    public class DrawToCommand : Command
    {
        private string xParameter;
        private string yParameter;

        public DrawToCommand(string xParameter, string yParameter)
        {
            this.xParameter = xParameter;
            this.yParameter = yParameter;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Extract the x and y values possibly from variables or direct numbers
            int x = context.GetVariableValue(xParameter);
            int y = context.GetVariableValue(yParameter);
            Point endPoint = new Point(x, y);

            // Draw the line on the canvas
            renderer.DrawLine(context.CurrentPosition, endPoint, context.CurrentColor);

            // Update the current position in the context to the end point of the line
            context.CurrentPosition = endPoint;

            // Update the pointer position to reflect the new position
            renderer.DrawPointer(endPoint);
        }
    }
}
