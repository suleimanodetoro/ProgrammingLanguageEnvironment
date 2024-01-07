using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CreateIntArrayCommand : Command
    {
        private string arrayName;
        private int[] values;

        public CreateIntArrayCommand(string arrayName, int[] values)
        {
            this.arrayName = arrayName;
            this.values = values;
        }

        public override void Execute(ICanvasRenderer renderer, ExecutionContext context)
        {
            if (context.IntArrays.ContainsKey(arrayName))
                throw new InvalidOperationException($"Array '{arrayName}' already exists.");

            context.IntArrays[arrayName] = values;
        }
    }

}
