using Moq;
using ProgrammingLanguageEnvironment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class VariableTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context;

        [TestInitialize]
        public void Setup()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext();
        }


        //The following is for testing varibale command 
        [TestMethod]
        public void Should_SetVariable_When_VariableCommandExecuted()
        {
            // Arrange
            var command = new VariableCommand("x", 10);

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(10, context.Variables["x"]);
        }


        //the following is for testing is variables are assigned to values
        [TestMethod]
        public void Should_UpdateVariable_When_AssignmentCommandExecuted()
        {
            // Arrange
            context.SetVariable("x", 10); // Initial value
            var command = new AssignmentCommand("x", "20"); // New value as direct integer

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(20, context.Variables["x"]);
        }


        //the following is for testing expression or redefinitions work
        [TestMethod]
        public void Should_EvaluateExpression_When_AssignmentCommandExecuted()
        {
            // Arrange
            context.SetVariable("x", 10); // Initial value
            var command = new AssignmentCommand("x", "x + 15"); // Increment x by 15

            // Act
            command.Execute(mockRenderer.Object, context);

            // Assert
            Assert.AreEqual(25, context.Variables["x"]);
        }



    }
}
