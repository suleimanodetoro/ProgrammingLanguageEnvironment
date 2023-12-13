using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class MethodData
    {
        public string MethodName { get; set; }
        public List<string> Parameters { get; set; }
        public List<Command> Commands { get; set; }

        public MethodData(string methodName, List<string> parameters)
        {
            MethodName = methodName;
            Parameters = parameters;
            Commands = new List<Command>();
        }
    }

}
