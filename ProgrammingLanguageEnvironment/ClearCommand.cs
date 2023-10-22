using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class ClearCommand : Command
    {
        public override void Execute(CanvasRenderer renderer)
        {
            renderer.ClearCanvas();
        }
    }

}
