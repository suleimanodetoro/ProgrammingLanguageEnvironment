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
    /// Contains unit tests for the TriangleCommand class to ensure that triangle commands are created and executed correctly.
    /// </summary>
    [TestClass]
    public class TriangleCommandTests
    {

        /// <summary>
        /// Verifies that the TriangleCommand constructor correctly assigns the side length parameter to its corresponding property.
        /// </summary>
        /// <remarks>
        /// This test checks that the side length provided to the constructor is accurately reflected in the TriangleCommand's SideLength property.
        /// </remarks>
        [TestMethod]
        public void TriangleCommand_CorrectParameters_CreatesCommand()
        {
            // Arrange
            int sideLength = 10;
            var triangleCommand = new TriangleCommand(sideLength);

            // Act & Assert
            Assert.AreEqual(sideLength, typeof(TriangleCommand)?.GetProperty("SideLength")?.GetValue(triangleCommand));
        }

        /// <summary>
        /// Ensures that executing the TriangleCommand invokes the DrawEquilateralTriangle method on the ICanvasRenderer interface with the correct side length.
        /// </summary>
        /// <remarks>
        /// A mock ICanvasRenderer is used to verify that DrawEquilateralTriangle is called with the side length specified when the command is executed.
        /// </remarks>
        [TestMethod]
        public void TriangleCommand_Execute_CallsDrawEquilateralTriangleWithCorrectSideLength()
        {
            // Arrange
            int sideLength = 10;
            var triangleCommand = new TriangleCommand(sideLength);
            var mockRenderer = new Mock<ICanvasRenderer>();
            mockRenderer.Setup(r => r.DrawEquilateralTriangle(It.IsAny<int>()));

            // Act
            triangleCommand.Execute(mockRenderer.Object);

            // Assert
            mockRenderer.Verify(r => r.DrawEquilateralTriangle(sideLength), Times.Once());
        }


        /// <summary>
        /// Tests that the TriangleCommand constructor throws an InvalidParameterException when a negative side length is provided.
        /// </summary>
        /// <remarks>
        /// A negative side length is invalid for a triangle, and the constructor must validate this input and throw an exception accordingly.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void TriangleCommand_NegativeSideLength_ThrowsInvalidParameterException()
        {
            // Arrange & Act
            var triangleCommand = new TriangleCommand(-10);

            // Assert is handled by ExpectedException
        }

        /// <summary>
        /// Tests that the TriangleCommand constructor throws an InvalidParameterException when a side length of zero is provided.
        /// </summary>
        /// <remarks>
        /// A side length of zero is not permissible for creating a valid triangle, and the constructor must throw an exception in such cases.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void TriangleCommand_ZeroSideLength_ThrowsInvalidParameterException()
        {
            // Arrange & Act
            var triangleCommand = new TriangleCommand(0);

            // Assert is handled by ExpectedException
        }

    }
}
