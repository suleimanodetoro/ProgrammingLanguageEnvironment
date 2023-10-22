using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    // Exception for invalid commands (Inherit from sytem exception definition)
    public class InvalidCommandException : Exception
    {
        public InvalidCommandException(string message) : base(message) { }
    }

    // Exception for invalid parameters (Inherit from sytem exception definition)
    public class InvalidParameterException : Exception
    {
        public InvalidParameterException(string message) : base(message) { }
    }

    // Exception for issues during command execution (Inherit from sytem exception definition)
    public class ExecutionException : Exception
    {
        public ExecutionException(string message) : base(message) { }
    }
}
