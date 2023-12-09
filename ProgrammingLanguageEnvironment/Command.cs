using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents the base class for all drawing commands.
    /// This is an abstract class that defines a common interface for all commands.
    /// </summary>
    public abstract class Command
    {
        /// <summary>
        /// When implemented in a derived class, executes the command using the provided canvas renderer.
        /// </summary>
        /// <param name="renderer">The canvas renderer that will execute the command.</param>
        public abstract void Execute(ICanvasRenderer renderer, ExecutionContext context);
    }
}
