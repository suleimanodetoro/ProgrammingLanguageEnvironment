using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to reset the drawing cursor to the origin (0,0) on the canvas.
    /// </summary>

    public class ResetCommand : Command
    {
        /// <summary>
        /// Executes the reset command on the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which to reset the drawing position.</param>
        /// <remarks>
        /// The execution of this command will move the current drawing position
        /// of the renderer back to the origin point (0,0) of the canvas.
        /// </remarks>
        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.MoveTo(new Point(0,0));
        }
    }
}
