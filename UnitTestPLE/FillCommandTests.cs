using Moq;
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    [TestClass]
    public class FillCommandTests
    {
        // Tests if creating a FillCommand with a specific state correctly sets the property.
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

            // Act
            fillCommand.Execute(mockRenderer.Object); // Execute the command with the mock renderer

            // Assert
            mockRenderer.Verify(r => r.SetFill(fillState), Times.Once(), "Execute should call SetFill on the renderer with the correct fill state exactly once.");
        }

        //No need to test invalid boolean arguement. It won't even let you run it is my reason behind this?
   
    }
}
