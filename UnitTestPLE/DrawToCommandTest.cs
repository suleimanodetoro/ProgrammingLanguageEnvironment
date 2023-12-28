using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment; 
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class DrawToCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context; //to avoid ambiguity

        [TestInitialize]
        public void Initialize()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext();  //to avoid ambiguity
            context.SetVariable("x", 30);  // Setting up variables for the test
            context.SetVariable("y", 40);
        }

        [TestMethod]
        public void DrawToCommand_Execute_UpdatesCurrentPosition()
        {
            // Arrange
            var drawToCommand = new DrawToCommand("x", "y");  // Using variable names instead of direct points
            Point expectedEndPoint = new Point(30, 40);       // Expected endpoint based on variable values

            // Set up mocking to capture line drawing
            mockRenderer.Setup(r => r.DrawLine(It.IsAny<Point>(), It.IsAny<Point>(), It.IsAny<Color>())).Verifiable();
            mockRenderer.Setup(r => r.DrawPointer(It.IsAny<Point>()));

            // Act
            drawToCommand.Execute(mockRenderer.Object, context);

            // Assert
            mockRenderer.Verify(r => r.DrawLine(It.IsAny<Point>(), expectedEndPoint, It.IsAny<Color>()), Times.Once());
            Assert.AreEqual(expectedEndPoint, context.CurrentPosition);  // Verify that the current position is updated
        }

        [TestMethod]
        public void DrawToCommand_NegativeCoordinates_ThrowsException()
        {
            // Set up context with negative coordinates
            context.SetVariable("x", -10);  // Negative value
            context.SetVariable("y", -20);  // Negative value

            var drawToCommand = new DrawToCommand("x", "y");

            // Act & Assert
            Assert.ThrowsException<ArgumentException>(() =>
            {
                drawToCommand.Execute(mockRenderer.Object, context);
            });
        }
    }
}
