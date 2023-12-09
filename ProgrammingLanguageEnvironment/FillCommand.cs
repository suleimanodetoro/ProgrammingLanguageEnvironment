using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to set the fill state of the canvas.
    /// </summary>
    public class FillCommand : Command
    {
        private bool fillState;

        /// <summary>
        /// Initializes a new instance of the <see cref="FillCommand"/> class with a specified fill state.
        /// </summary>
        /// <param name="state">The fill state to be applied to the canvas.</param>

        public FillCommand(bool state)
        {
            fillState = state;
        }


        /// <summary>
        /// Gets the current fill state of the command.
        /// </summary>
        public bool FillState => fillState;


        /// <summary>
        /// Executes the fill command on a given renderer, setting the canvas's fill state.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which the fill state will be set.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            renderer.SetFill(fillState);
        }
    }

}
