using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Class for a command to draw a rectangle on the canvas.
    /// </summary>
    public class RectangleCommand : Command
    {
        private string widthParameter;
        private string heightParameter;

        public RectangleCommand(string widthParameter, string heightParameter)
        {
            this.widthParameter = widthParameter;
            this.heightParameter = heightParameter;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            int width = context.GetVariableValue(widthParameter);
            int height = context.GetVariableValue(heightParameter);

            renderer.DrawRectangle(width, height, context.CurrentColor, context.CurrentPosition, context.FillShapes);
        }
    }

}
