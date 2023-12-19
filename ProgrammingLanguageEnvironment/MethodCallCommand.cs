using System.Collections.Generic;

namespace ProgrammingLanguageEnvironment
{
    public class MethodCallCommand : Command
    {
        private string methodName;
        private List<string> actualParameters;

        public MethodCallCommand(string methodName, List<string> actualParameters)
        {
            this.methodName = methodName;
            this.actualParameters = actualParameters;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            // Try to retrieve the method from the context
            if (context.Methods.TryGetValue(methodName, out var methodCommand))
            {
                // Create a new context for method execution
                var newContext = new ExecutionContext();

                // Copy existing variables to the new context
                foreach (var kvp in context.Variables)
                {
                    newContext.SetVariable(kvp.Key, kvp.Value);
                }

                // Map actual parameters to formal parameters defined in the method
                for (int i = 0; i < methodCommand.Parameters.Count; i++)
                {
                    int value = context.GetVariableValue(actualParameters[i]);
                    newContext.SetVariable(methodCommand.Parameters[i], value);
                }

                // Execute each command in the method's body with the new context
                foreach (var command in methodCommand.MethodBody)
                {
                    command.Execute(renderer, newContext);
                }
            }
            else
            {
                Console.WriteLine(context.Methods);
                // Throw an exception if the method is not found in the context
                throw new Exception($"Method '{methodName}' is not defined.");
            }
        }
    }
}
