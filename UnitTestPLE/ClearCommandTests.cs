using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class ClearCommandTests
    {
        [TestMethod]
        public void ClearCommand_InitializesSuccessfully()
        {
            // Arrange & Act: Create a ClearCommand object.
            var clearCommand = new ClearCommand();

            // Assert: Ensure the command object is not null.
            Assert.IsNotNull(clearCommand);
        }

        [TestMethod]
        public void ClearCommand_Execute_CallsClearOnRenderer()
        {
            // Arrange: Mock the ICanvasRenderer and create an instance of ExecutionContext.
            var mockRenderer = new Mock<ICanvasRenderer>();
            var mockContext = new Mock<ProgrammingLanguageEnvironment.ExecutionContext>();  // Use the full namespace to avoid ambiguity

            // Set up the mock renderer to expect a call to ClearCanvas.
            mockRenderer.Setup(r => r.ClearCanvas());

            // Instantiate the ClearCommand.
            var clearCommand = new ClearCommand();

            // Act: Execute the ClearCommand with the mock renderer and mock context.
            clearCommand.Execute(mockRenderer.Object, mockContext.Object);

            // Assert: Verify that ClearCanvas was called exactly once.
            mockRenderer.Verify(r => r.ClearCanvas(), Times.Once(), "ClearCanvas should be called once when ClearCommand executes.");
        }
    }
}
