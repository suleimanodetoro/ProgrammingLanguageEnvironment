using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class TriangleCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context;

        [TestInitialize]
        public void Setup()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext();
        }

        [TestMethod]
        public void TriangleCommand_WithValidSideLength_InitializesCorrectly()
        {
            // Arrange
            string sideLength = "10";
            var triangleCommand = new TriangleCommand(sideLength);

            // Act
            // Since sideLength is private, we can't directly check its value. Instead, we rely on correct execution.
            triangleCommand.Execute(mockRenderer.Object, context);

            // Assert
            // Verifying if DrawEquilateralTriangle method is called indicates that command has initialized correctly
            mockRenderer.Verify(r => r.DrawEquilateralTriangle(It.IsAny<Point[]>(), It.IsAny<Color>(), It.IsAny<bool>()), Times.Once());
        }

        [TestMethod]
        public void TriangleCommand_Execute_CallsDrawEquilateralTriangleWithCorrectParameters()
        {
            // Arrange
            context.SetVariable("side", 10); // Setting variable as if it was set by previous commands
            string sideLengthParameter = "side";
            var triangleCommand = new TriangleCommand(sideLengthParameter);

            mockRenderer.Setup(r => r.DrawEquilateralTriangle(It.IsAny<Point[]>(), It.IsAny<Color>(), It.IsAny<bool>()));

            // Act
            triangleCommand.Execute(mockRenderer.Object, context);

            // Assert
            mockRenderer.Verify(r => r.DrawEquilateralTriangle(It.IsAny<Point[]>(), It.IsAny<Color>(), context.FillShapes), Times.Once());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void TriangleCommand_InvalidSideLength_ThrowsException()
        {
            // Arrange
            string sideLength = "-10"; // Negative side length which is invalid
            var triangleCommand = new TriangleCommand(sideLength);

            // Act
            triangleCommand.Execute(mockRenderer.Object, context); // This should throw an exception

            // Assert is handled by ExpectedException
        }

    }
}
