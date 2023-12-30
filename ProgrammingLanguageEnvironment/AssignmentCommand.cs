using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents an assignment command that assigns a value to a variable.
    /// </summary>
    public class AssignmentCommand : Command
    {
        private string variableName;
        private string expression;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssignmentCommand"/> class.
        /// </summary>
        /// <param name="variableName">The name of the variable to assign to.</param>
        /// <param name="expression">The expression to evaluate and assign.</param>
        public AssignmentCommand(string variableName, string expression)
        {
            this.variableName = variableName;
            this.expression = expression;
        }

        /// <summary>
        /// Executes the assignment command by evaluating the expression and assigning the result to the variable.
        /// </summary>
        /// <param name="renderer">The canvas renderer (not used in assignment).</param>
        /// <param name="context">The current execution context.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Evaluate the expression
            int newValue = EvaluateExpression(expression, context);

            // Update or set the variable
            context.Variables[variableName] = newValue;
        }


        /// <summary>
        /// Evaluates a simple arithmetic expression.
        /// </summary>
        /// <param name="expr">The expression string.</param>
        /// <param name="context">The current execution context.</param>
        /// <returns>The result of the evaluated expression.</returns>
        private int EvaluateExpression(string expr, ExecutionContext context)
        {
            // Simple expression evaluation (e.g., 'x - 50')
            // This needs to be extended for more complex expressions
            var tokens = expr.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            // For simplicity, assuming expressions are in the form "x - 50" or "x + 50"
            if (tokens.Length == 3)
            {
                int varValue = context.Variables[tokens[0]];
                int operand = int.Parse(tokens[2]);

                switch (tokens[1])
                {
                    case "+":
                        return varValue + operand;
                    case "-":
                        return varValue - operand;
                    case "*":
                        return varValue * operand;
                    case "/":
                        if (operand == 0)
                        {
                            throw new Exception("Division by zero.");
                        }
                        return varValue / operand;
                    case "%":
                        return varValue % operand;
                    default:
                        throw new Exception("Unsupported operation in expression.");
                }
            }
            else if (tokens.Length == 1)
            {
                // Direct integer or variable value
                return context.Variables.ContainsKey(tokens[0]) ? context.Variables[tokens[0]] : int.Parse(tokens[0]);
            }
            else
            {
                throw new Exception("Invalid expression format.");
            }
        }
    }
}
