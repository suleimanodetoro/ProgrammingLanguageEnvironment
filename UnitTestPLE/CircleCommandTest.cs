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
    /// Contains tests for the CircleCommand class to ensure its proper functionality.
    /// </summary>
    [TestClass]
    public class CircleCommandTest
    {
        /// <summary>
        /// Tests whether a CircleCommand object is correctly instantiated with a valid radius.
        /// </summary>
        /// <remarks>
        /// A CircleCommand is expected to use the valid radius provided during instantiation.
        /// </remarks>
        [TestMethod]
        public void CircleCommand_ValidRadius_CreatesCommand()
        {
            // Arrange: Prepare the necessary variables and objects.
            // Set a valid radius for the circle.
            int radius = 10;

            // Act: Create a new CircleCommand with the specified radius.
            var circleCommand = new CircleCommand(radius);

            // Assert: The radius stored in CircleCommand should match the radius provided.
            Assert.AreEqual(radius, circleCommand.Radius, "The radius stored in CircleCommand should match the radius provided.");
        }

        /// <summary>
        /// Tests whether the Execute method of CircleCommand correctly calls the DrawCircle
        /// method of a CanvasRenderer with the correct radius.
        /// </summary>
        /// <remarks>
        /// This test uses a mock renderer to verify that the DrawCircle method is called
        /// with the correct radius when the Execute method of the CircleCommand is called.
        /// </remarks>
        [TestMethod]
        public void CircleCommand_Execute_CallsDrawCircleWithCorrectRadius()
        {
            // Arrange: Initialize the necessary objects and mock CanvasRenderer
            int radius = 10;
            var circleCommand = new CircleCommand(radius);
            var mockRenderer = new Mock<ICanvasRenderer>();
            // Setup a mock to track interactions with the DrawCircle method.
            mockRenderer.Setup(r => r.DrawCircle(It.IsAny<int>()));

            // Act: Execute the circle command using the mock renderer.
            circleCommand.Execute(mockRenderer.Object);

            // Assert: Verify that DrawCircle was called exactly once with the correct radius.
            mockRenderer.Verify(r => r.DrawCircle(radius), Times.Once(), "DrawCircle should be called once with the correct radius.");
        }



        /// <summary>
        /// Tests whether an InvalidParameterException is thrown when a CircleCommand is
        /// instantiated with a negative radius.
        /// </summary>
        /// <remarks>
        /// CircleCommand should not allow negative radii. This test ensures that an
        /// InvalidParameterException is thrown when trying to instantiate a CircleCommand
        /// with a negative radius.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException), "A negative radius should cause an InvalidParameterException to be thrown.")]
        public void CircleCommand_NegativeRadius_ThrowsInvalidParameterException()
        {
            // Arrange & Act: Try to create a CircleCommand with an invalid (negative) radius.
            var circleCommand = new CircleCommand(-10);

            // Assert: The ExpectedException attribute handles the assertion that an exception should be thrown.
        }

       
    }
}
