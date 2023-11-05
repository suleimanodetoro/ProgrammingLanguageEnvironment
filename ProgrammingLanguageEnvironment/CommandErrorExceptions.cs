using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents errors that occur due to an invalid command being issued to the system.
    /// Used extensively across application for exception handling
    /// </summary>
    // Exception for invalid commands (Inherit from sytem exception definition)
    public class InvalidCommandException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidCommandException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>

        public InvalidCommandException(string message) : base(message) { }
    }


    /// <summary>
    /// Represents errors that occur due to invalid parameters being passed to a command.
    /// </summary>
    public class InvalidParameterException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the InvalidParameterException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>

        public InvalidParameterException(string message) : base(message) { }
    }

/// <summary>
    /// Represents errors that occur during the execution of a command.
    /// </summary>
    public class ExecutionException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the ExecutionException class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>

        public ExecutionException(string message) : base(message) { }
    }
}
