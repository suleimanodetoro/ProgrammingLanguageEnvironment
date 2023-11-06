using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    /// <summary>
    /// Contains unit tests for the <see cref="ResetCommand"/> class.
    /// </summary>
    [TestClass]
    public class ResetCommandTests
    {
        /// <summary>
        /// Tests whether the <see cref="ResetCommand.Execute"/> method correctly invokes <see cref="ICanvasRenderer.MoveTo"/> with the origin point (0,0).
        /// </summary>
        [TestMethod]
        public void ResetCommand_Execute_ShouldInvokeMoveToWithOrigin()
        {
            // Arrange: Create a mock instance of ICanvasRenderer and instantiate the ResetCommand.
            var mockRenderer = new Mock<ICanvasRenderer>();
            var command = new ResetCommand();

            // Act: Execute the ResetCommand with the mocked renderer.
            command.Execute(mockRenderer.Object);

            // Assert: Verify that the MoveTo method was called on the mock object with a Point at (0,0), exactly once.
            mockRenderer.Verify(
                r => r.MoveTo(It.Is<Point>(p => p.X == 0 && p.Y == 0)),
                Times.Once(),
                "The MoveTo method should be called once with the point (0,0)."
            );
        }
    }

}
