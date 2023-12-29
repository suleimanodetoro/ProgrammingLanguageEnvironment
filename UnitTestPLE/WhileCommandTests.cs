using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using ExecutionContext = ProgrammingLanguageEnvironment.ExecutionContext;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class WhileCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ExecutionContext context;
        private WhileCommand whileCommand;
        private List<Command> loopCommands;

        [TestInitialize]
        public void Initialize()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ExecutionContext();
            loopCommands = new List<Command>();

            // Setting up initial condition
            context.SetVariable("x", 0);
            // Prepare a generic while command with an empty command list for now
            whileCommand = new WhileCommand("x < 5", loopCommands);
        }

        [TestMethod]
        public void WhileCommand_ExecutesUntilConditionIsFalse()
        {
            // Setup an increment command to increment 'x' within the loop.
            context.SetVariable("x", 0); // Ensure 'x' starts at 0

            // Create a command that increments 'x' each time it's executed.
            var incrementCommand = new Mock<Command>();
            incrementCommand.Setup(c => c.Execute(It.IsAny<ICanvasRenderer>(), It.IsAny<ExecutionContext>()))
                            .Callback(() => {
                                int x = context.GetVariableValue("x");
                                context.SetVariable("x", x + 1); // Ensure 'x' is incremented
                            });

            loopCommands.Clear();
            loopCommands.Add(incrementCommand.Object); // Add increment command to loop commands

            // Create a new WhileCommand with the updated loop commands
            whileCommand = new WhileCommand("x < 5", loopCommands);

            // Execute the while command
            whileCommand.Execute(mockRenderer.Object, context);

            // Asserting the loop ran 5 times, causing 'x' to increment from 0 to 5
            Assert.AreEqual(5, context.GetVariableValue("x"), "Loop should iterate until x is 5");
        }


        [TestMethod]
        public void WhileCommand_StopsWhenConditionInitiallyFalse()
        {
            // Set x to 5, so loop condition is false initially (x < 5)
            context.SetVariable("x", 5);

            // Execute the while command
            whileCommand.Execute(mockRenderer.Object, context);

            // Asserting no loop runs as 'x' starts at 5, making the condition initially false
            Assert.AreEqual(5, context.GetVariableValue("x"), "No loop iterations should occur if condition is initially false");
        }

        [TestMethod]
        public void WhileCommand_ExecutesInnerCommandsCorrectly()
        {
            // Resetting the context variable 'x' to 0
            context.SetVariable("x", 0);

            // Setup a simple inner command to track execution
            var innerCommandExecuted = 0;
            var incrementCommand = new Mock<Command>();
            incrementCommand.Setup(c => c.Execute(It.IsAny<ICanvasRenderer>(), It.IsAny<ExecutionContext>()))
                            .Callback(() => {
                                int x = context.GetVariableValue("x");
                                context.SetVariable("x", x + 1);  // Directly increment x in context
                                innerCommandExecuted++;  // Keeping track of execution count for the test
                            });

            loopCommands.Clear();
            loopCommands.Add(incrementCommand.Object); // Add mock command to loop commands

            // Execute the while command
            whileCommand = new WhileCommand("x < 5", loopCommands); // Ensure whileCommand uses the updated loopCommands
            whileCommand.Execute(mockRenderer.Object, context);

            // Asserting x was incremented 5 times from 0 to 5
            Assert.AreEqual(5, innerCommandExecuted, "Inner command should execute 5 times as per loop condition");
        }
    }
}
