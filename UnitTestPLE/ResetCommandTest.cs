using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class ResetCommandTests
    {
        [TestMethod]
        public void ResetCommand_Execute_ShouldInvokeMoveToWithOrigin()
        {
            // Arrange: Create a mock instance of ICanvasRenderer and ExecutionContext
            var mockRenderer = new Mock<ICanvasRenderer>();
            var mockContext = new Mock<ProgrammingLanguageEnvironment.ExecutionContext>(); // Explicitly specifying namespace
            var command = new ResetCommand();

            // Set up the renderer to expect a call to ResetPosition, which should internally call MoveTo with the origin point (0,0)
            mockRenderer.Setup(r => r.MoveTo(It.IsAny<Point>()));

            // Act: Execute the ResetCommand with the mocked renderer and context.
            command.Execute(mockRenderer.Object, mockContext.Object);

            // Assert: Verify that MoveTo was called with the origin (0,0)
            mockRenderer.Verify(r => r.MoveTo(It.Is<Point>(p => p.X == 0 && p.Y == 0)), Times.Once(), "MoveTo should be called with the origin (0,0)");
        }
    }
}
