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
    public class MoveToCommandTests
    {
        // Tests that the MoveToCommand initializes with a Point object representing the target position.
        // Asserts that the Point provided to the constructor is correctly assigned to the TargetPosition property.
        [TestMethod]
        public void MoveToCommand_WithValidCoordinates_InitializesCorrectly()
        {
            // Arrange: Create a Point object with X and Y coordinates to represent the target position.
            var targetPosition = new Point(50, 50);

            // Act: Instantiate a MoveToCommand object using the target position.
            var moveToCommand = new MoveToCommand(targetPosition);

            // Assert: Check that the TargetPosition property of the MoveToCommand object matches the point we passed to it.
            // This ensures that the constructor correctly initializes the property.
            Assert.AreEqual(targetPosition, moveToCommand.TargetPosition);
        }

        // Tests that executing the MoveToCommand calls the MoveTo method of the ICanvasRenderer interface.
        // It verifies that this method is called exactly once and with the correct Point parameter.
        //
        [TestMethod]
        public void MoveToCommand_Execute_CallsMoveToWithCorrectCoordinates()
        {
            // Arrange: Define a target position for our command and create a mock object of ICanvasRenderer.
            var targetPosition = new Point(50, 50);
            var moveToCommand = new MoveToCommand(targetPosition);
            var mockRenderer = new Mock<ICanvasRenderer>();
            // Setup a mock expectation that the MoveTo method will be called with any Point type argument.
            mockRenderer.Setup(r => r.MoveTo(It.IsAny<Point>()));

            // Act: Execute the moveToCommand with our mock renderer as the argument.
            moveToCommand.Execute(mockRenderer.Object);

            // Assert: Verify that the MoveTo method was called exactly one time with the target position we defined.
            // This ensures that the Execute method behaves as expected.
            mockRenderer.Verify(r => r.MoveTo(targetPosition), Times.Once());
        }

        // Tests that the MoveToCommand throws an InvalidParameterException when initialized with negative coordinates.
        // This exception should arise because negative coordinates are not valid for the MoveTo operation.
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void MoveToCommand_WithNegativeCoordinates_ThrowsException()
        {
            // Arrange & Act: Create a MoveToCommand object with negative coordinates which should result in an exception.
            // There is no separate 'Act' step here because the exception is expected to be thrown during object construction.
            var moveToCommand = new MoveToCommand(new Point(-10, -20));

            // Assert: The assertion is implicit here. The [ExpectedException] attribute above the method declaration
            // specifies the expected exception type. If that exception is not thrown, the test will fail.
        }
    }
}
