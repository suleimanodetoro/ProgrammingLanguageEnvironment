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
            int x = context.GetVariableValue(xParameter);
            int y = context.GetVariableValue(yParameter);
            Point endPoint = new Point(x, y);

            renderer.DrawLine(context.CurrentPosition, endPoint, context.CurrentColor);
            context.CurrentPosition = endPoint;
        }
    }
}
