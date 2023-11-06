using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to set the pen color on the canvas.
    /// </summary>
    public class PenCommand: Command
    {

        /// <summary>
        /// A set of valid color names.
        /// </summary>
        private Color penColor;
        private static readonly HashSet<string> ValidColors = new HashSet<string>
    {
        "red", "blue", "green", "black"
    };

        /// <summary>
        /// Initializes a new instance of the <see cref="PenCommand"/> class with the specified color name.
        /// </summary>
        /// <param name="colorName">The name of the color to set the pen to.</param>
        /// <exception cref="InvalidParameterException">
        /// Thrown if the color name provided is not valid.
        /// </exception>
        public PenCommand(string colorName)
        {
            if (!ValidColors.Contains(colorName.ToLower()))
            {
                throw new InvalidParameterException($"Invalid color provided for 'pen' command. Supported colors are: {string.Join(", ", ValidColors)}.");
            }
            penColor = Color.FromName(colorName);
        }

        /// <summary>
        /// Gets the color of the pen.
        /// </summary>
        public Color PenColor => penColor;

        /// <summary>
        /// Executes the pen color change command on the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which to set the pen color.</param>
        /// <remarks>
        /// The method calls the <c>SetPenColor</c> method on the renderer with the specified color.
        /// </remarks>
        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.SetPenColor(penColor);
        }


    }
}
