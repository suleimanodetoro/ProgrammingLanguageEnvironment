using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CircleCommand: ICommand
    {
        public int Radius {  get; }

        public CircleCommand(int radius)
        {
            Radius = radius;
        }

        public void Execute(CanvasRenderer renderer) {
            renderer.DrawCircle(Radius);
                }
    }
}
