using Moq;
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    [TestClass]
    public class PenCommandTests
    {

        // Test case for creating a PenCommand with a valid color.
        [TestMethod]
        public void PenCommand_ValidColor_CreatesCommandWithCorrectColor()
        {
            // Arrange: Set up a valid color name for the test.
            var colorName = "blue";

            // Act: Create a new PenCommand with the valid color name.
            var penCommand = new PenCommand(colorName);

            // Assert: Verify that the PenCommand has the correct Color object for the provided color name.
            Assert.AreEqual(Color.Blue, penCommand.PenColor, "The pen color should match the specified color name.");
        }


        // Test case for ensuring an exception is thrown when an invalid color is provided.
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void PenCommand_InvalidColor_ThrowsInvalidParameterException()
        {
            // Arrange: Define an invalid color name that is not supported.
            var invalidColorName = "purple"; //purple is not defined as a valid color in my codebase

            // Act & Assert: Create a PenCommand with the invalid color name, which should throw an InvalidParameterException.
            var penCommand = new PenCommand(invalidColorName);

            // Assert is handled by ExpectedException
        }


        // Test case for executing the PenCommand and verifying it calls the renderer with the correct color.

        [TestMethod]
        public void PenCommand_Execute_CallsSetPenColorOnRendererWithCorrectColor()
        {
            // Arrange
            var colorName = "red";
            var penCommand = new PenCommand(colorName);
            var mockRenderer = new Mock<ICanvasRenderer>();

            // Set up the mock to expect a call to SetPenColor with any Color parameter.
            mockRenderer.Setup(r => r.SetPenColor(It.IsAny<Color>()));

            // Act: Execute the PenCommand which should invoke SetPenColor on the renderer.
            penCommand.Execute(mockRenderer.Object);

            // Assert: Verify that SetPenColor was called exactly once with the correct color.
            mockRenderer.Verify(r => r.SetPenColor(Color.Red), Times.Once(), "The SetPenColor method should be called with the correct color.");
        }
    }
}
