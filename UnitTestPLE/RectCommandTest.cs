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
    public class RectCommandTests
    {
        // Test to ensure that the constructor correctly assigns the width and height.
        [TestMethod]
        public void RectangleCommand_CorrectParameters_CreatesCommand()
        {
            // Arrange
            int testWidth = 30;
            int testHeight = 40;

            // Act
            var rectangleCommand = new RectangleCommand(testWidth, testHeight);

            // Assert
            Assert.AreEqual(testWidth, rectangleCommand.Width, "The width should match the expected value.");
            Assert.AreEqual(testHeight, rectangleCommand.Height, "The height should match the expected value.");
        }

        // Test to ensure that the Execute method calls the DrawRectangle method on the renderer with correct parameters.
        [TestMethod]
        public void RectangleCommand_Execute_CallsDrawRectangleWithCorrectParameters()
        {
            // Arrange
            var width = 30;
            var height = 40;
            var rectangleCommand = new RectangleCommand(width, height);
            var mockRenderer = new Mock<ICanvasRenderer>();
            mockRenderer.Setup(r => r.DrawRectangle(It.IsAny<int>(), It.IsAny<int>()));

            // Act
            rectangleCommand.Execute(mockRenderer.Object);

            // Assert
            mockRenderer.Verify(r => r.DrawRectangle(width, height), Times.Once(), "The DrawRectangle method should be called with the correct width and height.");
        }

        // Test to ensure that the constructor throws an InvalidParameterException when negative values are passed.
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void RectangleCommand_NegativeParameters_ThrowsInvalidParameterException()
        {
            // Arrange & Act with negative values to trigger the exception
            var rectangleCommand = new RectangleCommand(-1, -1);

            // Assert is handled by ExpectedException
        }

    }
}
