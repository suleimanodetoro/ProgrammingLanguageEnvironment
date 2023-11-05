using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Moq;

using System.Threading.Tasks;

namespace UnitTestPLE
{
    /// <summary>
    /// Contains unit tests for the DrawToCommand class to verify its functionality.
    /// </summary>
    [TestClass]
    public class DrawToCommandTests
    {
        /// <summary>
        /// Verifies that a DrawToCommand object is correctly created with the specified endpoint.
        /// </summary>
        /// <remarks>
        /// This test ensures that the endpoint property of the created DrawToCommand object
        /// matches the point provided to the constructor.
        /// </remarks>
        [TestMethod]
        public void DrawToCommand_CorrectParameters_CreatesCommand()
        {
            // This test verifies that a DrawToCommand object is correctly created with the 39,40 endpoint.
            // It checks that the EndPoint property of the created DrawToCommand object matches the point provided to the constructor.

            // Arrange
            var point = new Point(30, 40);
            var drawToCommand = new DrawToCommand(point);

            // Act & Assert
            Assert.AreEqual(point, drawToCommand.EndPoint);
        }

        /// <summary>
        /// Ensures that executing the DrawToCommand calls the DrawLine method on the renderer with the correct endpoint.
        /// </summary>
        /// <remarks>
        /// This test uses mocking to intercept the call to the renderer and verify that DrawLine was called with the correct point.
        /// </remarks>
        [TestMethod]
        public void DrawToCommand_Execute_CallsDrawLineWithCorrectEndPoint()
        {
            // This test checks that the Execute method of the DrawToCommand calls the DrawLine method on the renderer with the correct endpoint.
            // It uses mocking to intercept the call to the renderer and verify that DrawLine was called with the correct point.

            // Arrange
            var endPoint = new Point(30, 40);
            var drawToCommand = new DrawToCommand(endPoint);
            var mockRenderer = new Mock<ICanvasRenderer>();
            mockRenderer.Setup(r => r.DrawLine(It.IsAny<Point>()));

            // Act
            drawToCommand.Execute(mockRenderer.Object);

            // Assert
            mockRenderer.Verify(r => r.DrawLine(endPoint), Times.Once());
        }

        /// <summary>
        /// Tests if constructing a DrawToCommand with negative coordinates throws an InvalidParameterException.
        /// </summary>
        /// <remarks>
        /// This test case uses the ExpectedException attribute to indicate that the test passes if the specified exception is thrown.
        /// </remarks>

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void DrawToCommand_NegativeCoordinates_ThrowsInvalidParameterException()
        {
            // This test ensures that an InvalidParameterException is thrown when a DrawToCommand is constructed with negative coordinates.
            // The ExpectedException attribute indicates the test will pass if the specified exception is thrown during execution.

            // Arrange & Act
            var drawToCommand = new DrawToCommand(new Point(-1, -1));

            // Assert is handled by ExpectedException
        }

       

    }
}
