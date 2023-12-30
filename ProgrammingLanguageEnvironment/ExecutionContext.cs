using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents the execution context of a script, holding the state and managing execution flow.
    /// </summary>
    public class ExecutionContext
    {
        /// <summary>Keeps track of variable values within the script.</summary>
        public Dictionary<string, int> Variables { get; private set; } = new Dictionary<string, int>();

        /// <summary>Stores a list of commands to be executed.</summary>
        public List<Command> Commands { get; set; } = new List<Command>();

        /// <summary>Indicates the current position in the command list.</summary>
        public int ProgramCounter { get; set; } = 0;

        /// <summary>Manages nested loops with their contexts.</summary>
        public Stack<LoopContext> LoopStack { get; private set; } = new Stack<LoopContext>();

        /// <summary>Stores defined methods and their associated commands.</summary>
        public Dictionary<string, MethodCommand> Methods { get; private set; } = new Dictionary<string, MethodCommand>();

        /// <summary>Represents the current position for drawing on the canvas.</summary>
        public Point CurrentPosition { get; set; } = new Point(0, 0);

        /// <summary>Current color used for drawing commands.</summary>
        public Color CurrentColor { get; set; } = Color.Black;

        /// <summary>Determines whether shapes are filled or not.</summary>
        public bool FillShapes { get; set; } = false;

        /// <summary>Sets the value of a variable.</summary>
        /// <param name="name">The variable's name.</param>
        /// <param name="value">The value to set.</param>

        public void SetVariable(string name, int value)
        {
            Variables[name] = value;
        }

        /// <summary>
        /// Retrieves the value of a variable or constant.
        /// </summary>
        /// <param name="variableOrConstant">The variable's name or a string representation of a constant.</param>
        /// <returns>The integer value of the variable or constant.</returns>
        /// <exception cref="Exception">Thrown when the variable or constant is not found or invalid.</exception>
        public int GetVariableValue(string variableOrConstant)
        {
            if (Variables.TryGetValue(variableOrConstant, out int value))
            {
                return value; // It's a variable with an integer value
            }
            else if (int.TryParse(variableOrConstant, out int constant))
            {
                return constant; // It's a directly provided integer
            }
            throw new Exception($"'{variableOrConstant}' is neither a valid variable nor an integer.");
        }

        /// <summary>
        /// Retrieves the value of a specific variable.
        /// </summary>
        /// <param name="name">The variable's name.</param>
        /// <returns>The integer value of the variable.</returns>
        /// <exception cref="Exception">Thrown when the variable is not found.</exception>

        public int GetVariable(string name)
        {
            if (Variables.TryGetValue(name, out int value))
            {
                return value;
            }
            throw new Exception($"Variable '{name}' not found.");
        }

        /// <summary>Adds a method to the execution context.</summary>
        /// <param name="methodName">The name of the method.</param>
        /// <param name="methodCommand">The method command to add.</param>

        public void AddMethod(string methodName, MethodCommand methodCommand)
        {
            if (Methods.ContainsKey(methodName))
            {
                throw new Exception($"A method with the name '{methodName}' is already defined.");
            }

            Methods[methodName] = methodCommand;
        }

        /// <summary>Retrieves a method command by name.</summary>
        /// <param name="methodName">The name of the method.</param>
        /// <returns>The method command associated with the given name.</returns>
        /// <exception cref="Exception">Thrown when the method is not defined.</exception>

        public MethodCommand GetMethod(string methodName)
        {
            if (Methods.TryGetValue(methodName, out var method))
            {
                return method;
            }

            throw new Exception($"Method '{methodName}' is not defined.");
        }

        /// <summary>Pushes a new loop context onto the loop stack.</summary>
        /// <param name="loopContext">The loop context to push.</param>

        public void PushLoopContext(LoopContext loopContext)
        {
            LoopStack.Push(loopContext);
        }

        /// <summary>Pops the current loop context from the stack.</summary>
        /// <returns>The popped loop context.</returns>
        public LoopContext PopLoopContext()
        {
            return LoopStack.Pop();
        }

        /// <summary>Gets the current loop context.</summary>
        /// <value>The current loop context at the top of the stack.</value>

        public LoopContext CurrentLoopContext => LoopStack.Peek();
    }

    /// <summary>
    /// Represents the context of a specific loop, including its start, end, and iterations.
    /// </summary>
    public class LoopContext
    {
        /// <summary>The index where the loop starts.</summary>
        public int StartIndex { get; set; }

        /// <summary>The index where the loop ends.</summary>
        public int EndIndex { get; set; }

        /// <summary>The total number of iterations for the loop.</summary>
        public int Iterations { get; set; }

        /// <summary>The current iteration number.</summary>
        public int CurrentIteration { get; set; } = 0;

        /// <summary>
        /// Constructs a new instance of a loop context with specified start, end, and iteration count.
        /// </summary>
        /// <param name="startIndex">The starting index of the loop.</param>
        /// <param name="endIndex">The ending index of the loop.</param>
        /// <param name="iterations">The total number of iterations.</param>

        public LoopContext(int startIndex, int endIndex, int iterations)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Iterations = iterations;
        }
    }
}
