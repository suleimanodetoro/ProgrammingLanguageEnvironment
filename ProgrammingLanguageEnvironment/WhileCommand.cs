using System;
using System.Collections.Generic;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a 'while' command that executes a block of commands repeatedly as long as a condition is true.
    /// </summary>
    public class WhileCommand : Command
    {
        private string condition;
        private List<Command> loopCommands;

        /// <summary>
        /// Initializes a new instance of the <see cref="WhileCommand"/> class with the specified condition and loop commands.
        /// </summary>
        /// <param name="condition">The condition to evaluate for the loop continuation.</param>
        /// <param name="loopCommands">The list of commands to execute in each iteration of the loop.</param>
        public WhileCommand(string condition, List<Command> loopCommands)
        {
            this.condition = condition;
            this.loopCommands = loopCommands;
        }


        /// <summary>
        /// Executes the 'while' command by continuously evaluating the condition and, if true, executing the loop's commands.
        /// </summary>
        /// <param name="renderer">The canvas renderer.</param>
        /// <param name="context">The current execution context.</param>
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

        /// <summary>
        /// Evaluates the specified condition within the given execution context.
        /// </summary>
        /// <param name="condition">The condition string.</param>
        /// <param name="context">The execution context providing variables and other state.</param>
        /// <returns>true if the condition evaluates to true; otherwise, false.</returns>
        private bool EvaluateCondition(string condition, ExecutionContext context)
        {
            // Split the condition into its parts: [operand1, operator, operand2]
            var parts = condition.Split(' ');
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
