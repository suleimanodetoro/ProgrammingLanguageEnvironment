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
        private Point endPoint;

        /// <summary>
        /// Initializes a new instance of the <see cref="DrawToCommand"/> class with a specified endpoint.
        /// </summary>
        /// <param name="endPoint">The endpoint where the line should be drawn to.</param>
        /// <exception cref="InvalidParameterException">
        /// Thrown when the coordinates of the endpoint are negative.
        /// </exception>
        public DrawToCommand(Point endPoint)
        {
            // Check if the provided Point is valid. For example, we can check if X and Y are non-negative.
            if (endPoint.X < 0 || endPoint.Y < 0)
            {
                throw new InvalidParameterException("Coordinates cannot be negative. Error in class file");
            }
            this.endPoint = endPoint;
        }

        /// <summary>
        /// Gets the endpoint where the line should be drawn to.
        /// </summary>
        public Point EndPoint => endPoint;


        /// <summary>
        /// Executes the draw to command on a given renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer where the line will be drawn to the endpoint.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            renderer.DrawLine(endPoint);
        }
    }
}
