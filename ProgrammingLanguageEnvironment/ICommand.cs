using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public interface ICommand
    {
        //By passing the CanvasRenderer as an argument to the Execute method,
        //I'm giving the command a reference to the renderer.
        //This allows the command to call methods on the renderer to perform drawing operations
        void Execute(CanvasRenderer renderer);

    }
}
