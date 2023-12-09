using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to clear the canvas.
    /// </summary>
    public class ClearCommand : Command
    {
        /// <summary>
        /// Executes the clear command on a given renderer, which clears the canvas.
        /// </summary>
        /// <param name="renderer">The canvas renderer that will have its canvas cleared.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            renderer.ClearCanvas();
        }
    }

}
