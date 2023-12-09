using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class VariableCommand : Command
    {
        private string variableName;
        private int value;

        public VariableCommand(string variableName, int value)
        {
            this.variableName = variableName;
            this.value = value;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
/*            context.SetVariable(variableName, value);
*/        }
    }
}
