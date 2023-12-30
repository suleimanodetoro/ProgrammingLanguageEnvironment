using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to create a variable or update its value.
    /// </summary>
    public class VariableCommand : Command
    {
        private string variableName;
        private int value;


        /// <summary>
        /// Initializes a new instance of the <see cref="VariableCommand"/> class.
        /// </summary>
        /// <param name="variableName">The name of the variable to create or update.</param>
        /// <param name="value">The value to assign to the variable.</param>

        public VariableCommand(string variableName, int value)
        {
            this.variableName = variableName;
            this.value = value;
        }


        /// <summary>
        /// Executes the variable command by setting the specified variable in the context to the specified value.
        /// </summary>
        /// <param name="renderer">The canvas renderer (not used in variable command).</param>
        /// <param name="context">The current execution context.</param>

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            context.SetVariable(variableName, value);
        }
    }
}
