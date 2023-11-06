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
        private int width;
        private int height;

        /// <summary>
        /// Initializes a new instance of the <see cref="RectangleCommand"/> class with the specified width and height.
        /// </summary>
        /// <param name="width">The width of the rectangle.</param>
        /// <param name="height">The height of the rectangle.</param>
        /// <exception cref="InvalidParameterException">
        /// Thrown if the width or height is less than or equal to zero.
        /// </exception>
        public RectangleCommand(int width, int height)
        {
            if (width <= 0 || height <= 0)
                throw new InvalidParameterException("Width and height for the rectangle must be positive values.");
            this.width = width;
            this.height = height;
        }
        /// <summary>
        /// Gets the width of the rectangle.
        /// </summary>
        public int Width => width;

        /// <summary>
        /// Gets the height of the rectangle.
        /// </summary>
        public int Height => height;


        /// <summary>
        /// Executes the rectangle drawing command on the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which to draw the rectangle.</param>
        /// <remarks>
        /// The method calls the <c>DrawRectangle</c> method on the renderer with the specified width and height.
        /// </remarks>
        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawRectangle(width, height);
        }
    }

}
