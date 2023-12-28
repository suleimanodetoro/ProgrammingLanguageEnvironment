using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class RectCommandTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ProgrammingLanguageEnvironment.ExecutionContext context; // Full namespace to avoid ambiguity

        [TestInitialize]
        public void Setup()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ProgrammingLanguageEnvironment.ExecutionContext(); // Full namespace to avoid ambiguity
        }

        [TestMethod]
        public void RectangleCommand_Execute_CallsDrawRectangleWithCorrectParameters()
        {
            // Arrange
            string widthParam = "30"; // Example width as a string
            string heightParam = "40"; // Example height as a string
            var rectangleCommand = new RectangleCommand(widthParam, heightParam);

            // Initialize variables in the context to simulate variable resolution
            context.SetVariable(widthParam, 30);
            context.SetVariable(heightParam, 40);

            mockRenderer.Setup(r => r.DrawRectangle(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()));

            // Act
            rectangleCommand.Execute(mockRenderer.Object, context);

            // Assert
            mockRenderer.Verify(r => r.DrawRectangle(30, 40, It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()), Times.Once(), "The DrawRectangle method should be called with the correct width and height.");
        }

        // When it comes to testing negative values, my RectangleCommand now accepts strings for dimensions, interpreted at runtime. 
        // I will shift my testing focus toward how my application handles invalid or undefined variables.
        // This means I can't directly test for negative dimensions at the constructor level as I used to.
        // I plan to test error handling and validation within the ExecutionContext or during the expression evaluation process instead.

    }
}
