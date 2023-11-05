using Moq;
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    /// <summary>
    /// Contains unit tests for the RectangleCommand class to ensure that rectangle commands are created and executed correctly.
    /// </summary>
    [TestClass]
    public class RectCommandTests
    {
        /// <summary>
        /// Verifies that the RectangleCommand constructor correctly assigns the width and height parameters to properties.
        /// </summary>
        /// <remarks>
        /// The test provides specific width and height values and asserts that the properties match these values after construction.
        /// </remarks>
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

        /// <summary>
        /// Ensures that executing the RectangleCommand invokes the DrawRectangle method on the ICanvasRenderer interface with the correct dimensions.
        /// </summary>
        /// <remarks>
        /// A mock ICanvasRenderer is used to verify that DrawRectangle is called with the width and height specified when the command is executed.
        /// </remarks>
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
