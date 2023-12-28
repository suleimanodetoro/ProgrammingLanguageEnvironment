using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Collections.Generic;
using System.Drawing;

using ExecutionContext = ProgrammingLanguageEnvironment.ExecutionContext;

namespace UnitTestPLE
{
    [TestClass]
    public class IfCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ExecutionContext context; // Now clearly refers to the right ExecutionContext to prevent ambiguous error
        private Command trueCommand; // Command to execute when condition is true
        private Command falseCommand; // Command to execute when condition is false

        [TestInitialize]
        public void Initialize()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ExecutionContext(); // Now clearly refers to your ExecutionContext

            // Initialize some commands to use in the tests
            trueCommand = new MoveToCommand("10", "20"); // Example true command
            falseCommand = new ResetCommand(); // Example false command or alternative command

            // Setup context with variables or conditions needed for testing
            context.SetVariable("x", 5);
            context.SetVariable("y", 10);
        }

        //Test if command when if condition is true

        [TestMethod]
        public void IfCommand_ShouldExecuteCommands_WhenConditionIsTrue()
        {
            // Arrange
            var ifCommands = new List<Command> { trueCommand };
            var ifCommand = new IfCommand("x < y", ifCommands);

            // Act
            ifCommand.Execute(mockRenderer.Object, context);

            // Assert
            // Since trueCommand is a MoveToCommand with "10", "20", you would check the context's CurrentPosition
            Assert.AreEqual(new Point(10, 20), context.CurrentPosition, "The context's position should be updated by the trueCommand.");
        }

        // The following is for when IfCommand gets False Condition
        [TestMethod]
        public void IfCommand_ShouldNotExecuteCommands_WhenConditionIsFalse()
        {
            // Arrange
            context.SetVariable("x", 20); // Change the condition to be false
            var ifCommands = new List<Command> { trueCommand };
            var ifCommand = new IfCommand("x < y", ifCommands);

            // Act
            ifCommand.Execute(mockRenderer.Object, context);

            // Assert
            // Check that the context's position is unchanged
            Assert.AreNotEqual(new Point(10, 20), context.CurrentPosition, "The context's position should not be updated by the trueCommand.");
        }
        //The following test is for IfCommand throwing exception on Invalid Condition
        [TestMethod]
        [ExpectedException(typeof(Exception), "An invalid condition format should throw an exception.")]
        public void IfCommand_ShouldThrowException_OnInvalidCondition()
        {
            // Arrange
            var ifCommands = new List<Command> { trueCommand };
            var ifCommand = new IfCommand("invalid condition", ifCommands);

            // Act & Assert
            ifCommand.Execute(mockRenderer.Object, context);
        }





    }
}
