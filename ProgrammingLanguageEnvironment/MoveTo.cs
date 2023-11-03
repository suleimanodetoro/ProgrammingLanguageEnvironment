using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class MoveToCommand: Command
    {
        private Point targetPosition;

        public MoveToCommand(Point target)
        {
            // Check if the provided Point is valid. For example, we can check if X and Y are non-negative.
            if (target.X < 0 || target.Y < 0)
            {
                throw new InvalidParameterException("Coordinates cannot be negative. Error in class file");
            }
            this.targetPosition = target; 
        }

        public override void Execute(ICanvasRenderer renderer)
        {
            renderer.MoveTo(targetPosition);
        }
        public Point TargetPosition => targetPosition;

    }
}
