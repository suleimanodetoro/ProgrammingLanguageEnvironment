using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class MoveToCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context; // Use the full namespace

        [TestInitialize]
        public void Setup()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext(); // Use the full namespace
        }

        [TestMethod]
        public void MoveToCommand_Execute_CallsMoveToWithCorrectCoordinates()
        {
            // Arrange
            string xParam = "50";
            string yParam = "50";
            var moveToCommand = new MoveToCommand(xParam, yParam);

            // Act
            moveToCommand.Execute(mockRenderer.Object, context);

            // Assert
            var expectedPoint = new Point(50, 50);
            mockRenderer.Verify(r => r.MoveTo(It.Is<Point>(p => p.Equals(expectedPoint))), Times.Once());
        }

        // Test to ensure negative coordinate handling or other validation will be added later 
        // Since  commands are now string-based and interpreted at runtime, the command's constructor won't throw exceptions for invalid coordinates.
        // Instead,  test for how the application handles invalid or undefined variables at execution time.
    }
}
