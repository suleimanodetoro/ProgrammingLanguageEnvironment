using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// class represents a command to move the pen to a specified position on the canvas.
    /// </summary>
    public class MoveToCommand: Command
    {
        /// <summary>
        /// target point variable definition
        /// </summary>
        private Point targetPosition;
        /// <summary>
        /// Initializes a new instance of the <see cref="MoveToCommand"/> class with the target position.
        /// </summary>
        /// <param name="target">The <see cref="Point"/> representing the target position.</param>
        /// <exception cref="InvalidParameterException">
        /// Thrown when the provided coordinates are negative.
        /// </exception>

        public MoveToCommand(Point target)
        {
            // Check if the provided Point is valid. For example, we can check if X and Y are non-negative.
            if (target.X < 0 || target.Y < 0)
            {
                throw new InvalidParameterException("Coordinates cannot be negative. Error in class file");
            }
            this.targetPosition = target; 
        }

        /// <summary>
        /// Gets the target position where the pen will be moved.
        /// </summary>
        public Point TargetPosition => targetPosition;


        /// <summary>
        /// Executes the command to move the pen to the target position on the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer on which to move the pen.</param>
        /// <remarks>
        /// The method calls the <c>MoveTo</c> method on the renderer with the target position.
        /// </remarks>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            renderer.MoveTo(targetPosition);
        }

    }
}
