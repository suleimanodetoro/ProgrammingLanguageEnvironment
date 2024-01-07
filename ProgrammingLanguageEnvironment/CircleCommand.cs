using System;

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
            int radius = GetRadius(context);
            renderer.DrawCircle(radius, context.CurrentColor, context.CurrentPosition, context.FillShapes);
        }

        private int GetRadius(ExecutionContext context)
        {
            // Using the ArrayUtilities class to check and parse array access
            if (ArrayUtilities.IsArrayAccess(radiusParameter))
            {
                var (arrayName, index) = ArrayUtilities.ParseArrayAccess(radiusParameter);
                if (context.IntArrays.TryGetValue(arrayName, out int[] array))
                {
                    if (index >= 0 && index < array.Length)
                    {
                        return array[index]; // Return the array element as radius
                    }
                    else
                    {
                        throw new Exception($"Index '{index}' is out of bounds for array '{arrayName}'.");
                    }
                }
                else
                {
                    throw new Exception($"Array '{arrayName}' not found.");
                }
            }
            else if (context.Variables.TryGetValue(radiusParameter, out int value))
            {
                // The parameter is a direct variable
                return value;
            }
            else if (int.TryParse(radiusParameter, out int directValue))
            {
                // The parameter is a direct integer value
                return directValue;
            }
            else
            {
                throw new InvalidParameterException("Invalid radius provided for 'circle'.");
            }
        }
    }
}
