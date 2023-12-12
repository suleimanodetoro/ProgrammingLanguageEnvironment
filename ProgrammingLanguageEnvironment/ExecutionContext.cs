﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class ExecutionContext
    {
        public Dictionary<string, int> Variables { get; private set; } = new Dictionary<string, int>();
        public List<Command> Commands { get; set; } = new List<Command>();
        public int ProgramCounter { get; set; } = 0;
        // Loop handling
        public Stack<LoopContext> LoopStack { get; private set; } = new Stack<LoopContext>();


        // Add methods for variable and loop handling
        public void SetVariable(string name, int value)
        {
            Variables[name] = value;
        }

        public int GetVariableValue(string variableOrConstant)
        {
            if (int.TryParse(variableOrConstant, out int constantValue))
            {
                return constantValue; // It's a constant
            }
            else if (Variables.TryGetValue(variableOrConstant, out int variableValue))
            {
                return variableValue; // It's a variable
            }
            throw new Exception($"Variable '{variableOrConstant}' not found or invalid constant.");
        }

        public int GetVariable(string name)
        {
            if (Variables.TryGetValue(name, out int value))
            {
                return value;
            }
            throw new Exception($"Variable '{name}' not found.");
        }
        public void PushLoopContext(LoopContext loopContext)
        {
            LoopStack.Push(loopContext);
        }

        public LoopContext PopLoopContext()
        {
            return LoopStack.Pop();
        }

        public LoopContext CurrentLoopContext => LoopStack.Peek();
    }

    public class LoopContext
    {
        public int StartIndex { get; set; }
        public int EndIndex { get; set; }
        public int Iterations { get; set; }
        public int CurrentIteration { get; set; } = 0;

        public LoopContext(int startIndex, int endIndex, int iterations)
        {
            StartIndex = startIndex;
            EndIndex = endIndex;
            Iterations = iterations;
        }
    }
}
