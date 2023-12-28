using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;  
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class PenCommandTests
    {
        [TestMethod]
        public void PenCommand_ValidColor_CreatesCommandWithCorrectColor()
        {
            // Arrange
            var colorName = "blue";  // Assuming "blue" is a valid color

            // Act
            var penCommand = new PenCommand(colorName);

            // Assert
            Assert.AreEqual(Color.Blue, penCommand.PenColor, "The pen color should be blue for the 'blue' input.");
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void PenCommand_InvalidColor_ThrowsInvalidParameterException()
        {
            // Arrange
            var invalidColorName = "purple";  // Assuming "purple" is not a valid color

            // Act & Assert
            var penCommand = new PenCommand(invalidColorName);
        }

        [TestMethod]
        public void PenCommand_Execute_CallsSetPenColorOnRendererWithCorrectColor()
        {
            // Arrange
            var colorName = "red";  // Assuming "red" is a valid color
            var penCommand = new PenCommand(colorName);
            var mockRenderer = new Mock<ICanvasRenderer>();
            var mockContext = new Mock<ProgrammingLanguageEnvironment.ExecutionContext>();  

            // Act
            penCommand.Execute(mockRenderer.Object, mockContext.Object);

            // Assert
            // Verify that the CurrentColor of the ExecutionContext is set to the correct color.
            mockContext.VerifySet(ctx => ctx.CurrentColor = Color.Red, Times.Once(), "The CurrentColor should be set to red.");
        }
    }
}
