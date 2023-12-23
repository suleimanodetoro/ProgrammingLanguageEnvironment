using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// This class represents a command to draw a circle on the canvas.
    /// </summary>
    public class CircleCommand : Command
    {
        private string radiusParameter;

        public CircleCommand(string radiusParameter)
        {
            this.radiusParameter = radiusParameter;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            int radius;
            // Check if the parameter is a variable or a direct value
            if (context.Variables.ContainsKey(radiusParameter))
            {
                radius = context.GetVariable(radiusParameter);
            }
            else if (!int.TryParse(radiusParameter, out radius))
            {
                throw new InvalidParameterException("Invalid radius provided for 'circle'.");
            }

            // Use the ExecutionContext's color and position
            renderer.DrawCircle(radius, context.CurrentColor, context.CurrentPosition, context.FillShapes);
        }
    }

}

