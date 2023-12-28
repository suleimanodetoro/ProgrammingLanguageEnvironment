using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class FillCommandTests
    {
        [TestMethod]
        public void FillCommand_SetState_CorrectlySetsProperty()
        {
            // Arrange
            bool fillState = true; // Example state to test with

            // Act
            var fillCommand = new FillCommand(fillState);

            // Assert
            Assert.AreEqual(fillState, fillCommand.FillState, "The FillState property should match the value passed to the constructor.");
        }

        [TestMethod]
        public void FillCommand_Execute_CallsSetFillWithCorrectState()
        {
            // Arrange
            bool fillState = false; // Example state to test with
            var fillCommand = new FillCommand(fillState);
            var mockRenderer = new Mock<ICanvasRenderer>(); // Create a mock of the ICanvasRenderer
            mockRenderer.Setup(r => r.SetFill(It.IsAny<bool>())); // Setup the mock to expect a call to SetFill

            var context = new ProgrammingLanguageEnvironment.ExecutionContext(); // Use the full namespace to avoid ambiguity

            // Act
            fillCommand.Execute(mockRenderer.Object, context); // Execute the command with the mock renderer and the specified context

            // Assert
            mockRenderer.Verify(r => r.SetFill(fillState), Times.Once(), "Execute should call SetFill on the renderer with the correct fill state exactly once.");
        }
    }
}
