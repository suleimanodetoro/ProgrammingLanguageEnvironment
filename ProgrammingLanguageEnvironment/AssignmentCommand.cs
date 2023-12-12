using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{

    public class AssignmentCommand : Command
    {
        private string variableName;
        private string expression;

        public AssignmentCommand(string variableName, string expression)
        {
            this.variableName = variableName;
            this.expression = expression;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Evaluate the expression
            int newValue = EvaluateExpression(expression, context);

            // Update or set the variable
            context.Variables[variableName] = newValue;
        }

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
