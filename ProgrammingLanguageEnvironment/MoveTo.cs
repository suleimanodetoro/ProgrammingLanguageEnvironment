using System;
using System.Drawing; 
namespace ProgrammingLanguageEnvironment
{
    public class MoveToCommand : Command
    {
        private string xParameter;
        private string yParameter;

        public MoveToCommand(string xParameter, string yParameter)
        {
            this.xParameter = xParameter;
            this.yParameter = yParameter;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Resolve X and Y from context or parse from string
            int x = context.GetVariableValue(xParameter); 
            int y = context.GetVariableValue(yParameter);

            Point actualTargetPosition = new Point(x, y);
            context.CurrentPosition = actualTargetPosition;
            renderer.MoveTo(actualTargetPosition);
        }
    }
}
