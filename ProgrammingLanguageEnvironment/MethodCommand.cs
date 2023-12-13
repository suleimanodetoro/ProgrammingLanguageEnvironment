using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class MethodCommand : Command
    {
        public string MethodName { get; }
        public List<string> Parameters { get; }
        public List<Command> MethodBody { get; }

        public MethodCommand(string methodName, List<string> parameters, List<Command> methodBody)
        {
            MethodName = methodName;
            Parameters = parameters;
            MethodBody = methodBody;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            context.AddMethod(MethodName, this);
        }

    }

}