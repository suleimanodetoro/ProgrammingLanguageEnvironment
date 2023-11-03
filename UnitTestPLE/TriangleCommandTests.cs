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
    public class TriangleCommandTests
    {
        [TestMethod]
        public void TriangleCommand_CorrectParameters_CreatesCommand()
        {
            // Arrange
            int sideLength = 10;
            var triangleCommand = new TriangleCommand(sideLength);

            // Act & Assert
            Assert.AreEqual(sideLength, typeof(TriangleCommand)?.GetProperty("SideLength")?.GetValue(triangleCommand));
        }

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

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void TriangleCommand_NegativeSideLength_ThrowsInvalidParameterException()
        {
            // Arrange & Act
            var triangleCommand = new TriangleCommand(-10);

            // Assert is handled by ExpectedException
        }

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
