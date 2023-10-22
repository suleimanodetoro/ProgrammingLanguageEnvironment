using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class FillCommand : Command
    {
        private bool fillState;

        public FillCommand(bool state)
        {
            fillState = state;
        }

        public bool FillState => fillState;

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.SetFill(fillState);
        }
    }

}
