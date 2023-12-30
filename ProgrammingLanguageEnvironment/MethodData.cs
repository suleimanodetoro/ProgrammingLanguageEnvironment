using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents the data structure for storing method information.
    /// </summary>
    public class MethodData
    {
        /// <summary>Gets or sets the method name.</summary>
        public string MethodName { get; set; }

        /// <summary>Gets or sets the list of parameter names.</summary>
        public List<string> Parameters { get; set; }

        /// <summary>Gets or sets the list of commands that form the method body.</summary>
        public List<Command> Commands { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MethodData"/> class.
        /// </summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="parameters">The list of parameters for the method.</param>
        public MethodData(string methodName, List<string> parameters)
        {
            MethodName = methodName;
            Parameters = parameters;
            Commands = new List<Command>();
        }
    }

}
