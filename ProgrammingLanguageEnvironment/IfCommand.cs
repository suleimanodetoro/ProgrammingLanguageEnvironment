using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents an 'if' command that conditionally executes a block of commands.
    /// </summary>
    public class IfCommand : Command
    {
        private string condition;
        private List<Command> ifCommands;


        /// <summary>
        /// Initializes a new instance of the <see cref="IfCommand"/> class with the specified condition and commands to execute if the condition is true.
        /// </summary>
        /// <param name="condition">The condition to evaluate.</param>
        /// <param name="ifCommands">The list of commands to execute if the condition is true.</param>
        public IfCommand(string condition, List<Command> ifCommands)
        {
            this.condition = condition;
            this.ifCommands = ifCommands;
        }


        /// <summary>
        /// Executes the 'if' command by evaluating the condition and, if true, executing the associated commands.
        /// </summary>
        /// <param name="renderer">The canvas renderer.</param>
        /// <param name="context">The current execution context.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Evaluate the condition; if true, execute all commands within the 'if' block
            if (EvaluateCondition(condition, context))
            {
                foreach (var command in ifCommands)
                {
                    command.Execute(renderer, context);
                }
            }
        }


        /// <summary>
        /// Evaluates the specified condition within the given execution context.
        /// </summary>
        /// <param name="condition">The condition string.</param>
        /// <param name="context">The execution context providing variables and other state.</param>
        /// <returns>true if the condition evaluates to true; otherwise, false.</returns>
        private bool EvaluateCondition(string condition, ExecutionContext context)
        {
            // Split the condition into its parts: [operand1, operator, operand2]
            var parts = condition.Trim().Split(' ');
            if (parts.Length != 3)
                throw new Exception("Invalid condition format.");

            int leftOperand = context.GetVariableValue(parts[0]);
            int rightOperand = context.GetVariableValue(parts[2]);


            // Evaluate the condition based on the operator and return the result
            switch (parts[1])
            {
                case "<": return leftOperand < rightOperand;
                case ">": return leftOperand > rightOperand;
                default: throw new Exception("Unsupported operation in condition.");
            }
        }

    }
}
