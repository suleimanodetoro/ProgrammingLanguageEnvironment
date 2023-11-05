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
        private readonly int sideLength;

        /// <summary>
        /// Initializes a new instance of the <see cref="TriangleCommand"/> class.
        /// </summary>
        /// <param name="sideLength">The length of each side of the equilateral triangle.</param>
        /// <exception cref="InvalidParameterException">Thrown when the side length is less than or equal to zero.</exception>
        public TriangleCommand(int sideLength)
        {
            if (sideLength<= 0)
                throw new InvalidParameterException("Side length cannot be negative or zero.");
            this.sideLength = sideLength;
        }

        /// <summary>
        /// Gets the length of the sides of the triangle.
        /// </summary>
        public int SideLength => sideLength; // Public getter for the side length - aid for testingggg


        /// <summary>
        /// Executes the triangle drawing command on the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which to draw the triangle.</param>
        /// <remarks>
        /// The method calls the <c>DrawEquilateralTriangle</c> method on the renderer with the specified side length.
        /// </remarks>
        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.DrawEquilateralTriangle(sideLength);
        }
    }

}
