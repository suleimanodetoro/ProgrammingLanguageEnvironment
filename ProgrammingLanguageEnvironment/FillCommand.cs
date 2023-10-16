using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class FillCommand: ICommand
    {
        public bool IsFillEnabled { get; }

        public FillCommand(string fillOption)
        {
            IsFillEnabled = fillOption.ToLower()=="on";
        }

        public void Execute(CanvasRenderer renderer)
        {
            renderer.SetFill(IsFillEnabled);
        }
    }
}
