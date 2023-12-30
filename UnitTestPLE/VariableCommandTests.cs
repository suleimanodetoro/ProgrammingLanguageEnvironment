using Moq;
using ProgrammingLanguageEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace UnitTestPLE
{
    /// <summary>
    /// Test class for verifying variable-related functionality within the programming language environment.
    /// </summary>
    [TestClass]
    public class VariableTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context;


        /// <summary>
        /// Initializes variables and mocks before each test.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext();
        }


        /// <summary>
        /// Verifies that the VariableCommand sets a variable within the execution context.
        /// </summary>
        [TestMethod]
        public void Should_SetVariable_When_VariableCommandExecuted()
        {
            // Arrange
            var command = new VariableCommand("x", 10);

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(10, context.Variables["x"], "Variable x should be set to 10");
        }


        /// <summary>
        /// Tests if the AssignmentCommand updates the value of a variable.
        /// </summary>
        [TestMethod]
        public void Should_UpdateVariable_When_AssignmentCommandExecuted()
        {
            // Arrange
            context.SetVariable("x", 10); // Initial value
            var command = new AssignmentCommand("x", "20"); // New value as direct integer

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(20, context.Variables["x"], "Variable x should be updated to 20");
        }


        /// <summary>
        /// Ensures that AssignmentCommand correctly evaluates and assigns expressions to variables.
        /// </summary>
        [TestMethod]
        public void Should_EvaluateExpression_When_AssignmentCommandExecuted()
        {
            // Arrange
            context.SetVariable("x", 10); // Initial value
            var command = new AssignmentCommand("x", "x + 15"); // Expression to increment x by 15

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(25, context.Variables["x"], "Variable x should be incremented to 25");
        }



    }
}
