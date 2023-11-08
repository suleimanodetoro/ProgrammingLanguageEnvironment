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
        /// <summary>
        /// The radius of the circle to be drawn.
        /// </summary>
        private int radius;

        /// <summary>
        /// Initializes a new instance of the <see cref="CircleCommand"/> class with the specified radius.
        /// </summary>
        /// <param name="radius">The radius of the circle. Must be greater than zero.</param>
        /// <exception cref="InvalidParameterException">Thrown when the radius is less than or equal to zero.</exception>
        public CircleCommand(int radius)
        {
            if (radius <= 0)
                throw new InvalidParameterException($"Invalid radius for 'circle'. Radius must be positive. Received: {radius}");

            this.radius = radius;
        }

        /// <summary>
        /// Getter the radius of the circle.
        /// </summary>
        public int Radius => radius;

        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawCircle(radius);
        }
    }

}
