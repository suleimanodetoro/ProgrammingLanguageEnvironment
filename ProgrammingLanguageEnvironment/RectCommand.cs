using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class RectCommand: ICommand
    {

        public int Width { get; }
        public int Height { get; }
        public RectCommand(int width, int height) 
        { 
            Width = width;
            Height = height;
        }

        public void Execute(CanvasRenderer renderer) 
        {
            renderer.DrawRectangle(Width, Height);
        }
    }
}
