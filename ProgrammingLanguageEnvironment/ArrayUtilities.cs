using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public static class ArrayUtilities
    {
        public static bool IsArrayAccess(string parameter)
        {
            return parameter.Contains("[") && parameter.Contains("]");
        }

        public static (string arrayName, int index) ParseArrayAccess(string arrayAccess)
        {
            var parts = arrayAccess.Split(new char[] { '[', ']' }, StringSplitOptions.RemoveEmptyEntries);
            var arrayName = parts[0];
            var index = int.Parse(parts[1]); // TODO: add some error handling here
            return (arrayName, index);
        }
    }

}
