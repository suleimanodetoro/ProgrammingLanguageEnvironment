using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class MoveToCommand: ICommand
    {
        public Point Target { get; }
        public MoveToCommand(int x,int y)
        {  
            Target = new Point(x,y); 
        }

        public void Execute(CanvasRenderer renderer)
        {
            renderer.MoveTo(Target);
        }
    }
}
