using System;
using System.Collections.Generic;

namespace ProgrammingLanguageEnvironment
{
    public class WhileCommand : Command
    {
        private string condition;
        private List<Command> loopCommands;

        // Constructor
        public WhileCommand(string condition, List<Command> loopCommands)
        {
            this.condition = condition;
            this.loopCommands = loopCommands;
        }

        // Execute method
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Continuously evaluates the loop condition and executes the loop's commands
            while (EvaluateCondition(condition, context))
            {
                foreach (var command in loopCommands)
                {
                    command.Execute(renderer, context);
                }
            }
        }

        // EvaluateCondition method
        private bool EvaluateCondition(string condition, ExecutionContext context)
        {
            var parts = condition.Split(' ');
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
