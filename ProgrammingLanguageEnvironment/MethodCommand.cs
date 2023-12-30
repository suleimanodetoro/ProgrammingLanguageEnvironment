using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents a command to define a method.
    /// </summary>
    public class MethodCommand : Command
    {
        /// <summary>Gets the name of the method.</summary>
        public string MethodName { get; }

        /// <summary>Gets the list of parameters for the method.</summary>
        public List<string> Parameters { get; }

        /// <summary>Gets the list of commands that form the method body.</summary>
        public List<Command> MethodBody { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodCommand"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <param name="methodBody">The list of commands comprising the method body.</param>
        public MethodCommand(string methodName, List<string> parameters, List<Command> methodBody)
        {
            MethodName = methodName;
            Parameters = parameters;
            MethodBody = methodBody;
        }

        /// <summary>
        /// Executes the method command, adding the method to the execution context.
        /// </summary>
        /// <param name="renderer">The canvas renderer.</param>
        /// <param name="context">The current execution context.</param>
        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            context.AddMethod(MethodName, this);
        }

    }

}