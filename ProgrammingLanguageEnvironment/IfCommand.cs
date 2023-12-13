using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class IfCommand : Command
    {
        private string condition;
        private List<Command> ifCommands;

        public IfCommand(string condition, List<Command> ifCommands)
        {
            this.condition = condition;
            this.ifCommands = ifCommands;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            if (EvaluateCondition(condition, context))
            {
                foreach (var command in ifCommands)
                {
                    command.Execute(renderer, context);
                }
            }
        }

        private bool EvaluateCondition(string condition, ExecutionContext context)
        {
            var parts = condition.Trim().Split(' ');
            if (parts.Length != 3)
                throw new Exception("Invalid condition format.");

            int leftOperand = context.GetVariableValue(parts[0]);
            int rightOperand = context.GetVariableValue(parts[2]);

            switch (parts[1])
            {
                case "<": return leftOperand < rightOperand;
                case ">": return leftOperand > rightOperand;
                default: throw new Exception("Unsupported operation in condition.");
            }
        }

    }
}
