using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class CircleCommandTest
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
        public void CircleCommand_Execute_CallsDrawCircleWithCorrectRadius()
        {
            // Arrange
            string radiusParam = "10"; // Example radius as a string representing a direct value
            var circleCommand = new CircleCommand(radiusParam);

            // Initialize variables in the context to simulate variable resolution
            context.SetVariable(radiusParam, 10);

            mockRenderer.Setup(r => r.DrawCircle(It.IsAny<int>(), It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()));

            // Act
            circleCommand.Execute(mockRenderer.Object, context);

            // Assert
            mockRenderer.Verify(r => r.DrawCircle(10, It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()), Times.Once(), "The DrawCircle method should be called with the correct radius.");
        }

        // Note on Negative Values and Invalid Inputs:
        // I'm aware that the CircleCommand now accepts strings that are interpreted at runtime.
        // Therefore, testing for negative or invalid values at the constructor level isn't directly feasible.
        // Instead, I'll focus on testing how the application handles invalid or undefined variables at runtime,
        // ensuring robust error handling and validation within the ExecutionContext or during variable resolution.
    }
}
