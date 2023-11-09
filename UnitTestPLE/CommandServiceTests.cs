using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;

// The namespace containing the unit tests for the ProgrammingLanguageEnvironment.
namespace UnitTestPLE
{
    /// <summary>
    /// The CommandServiceTests class contains unit tests for testing the CommandService's interaction
    /// with its dependencies, ensuring they are called as expected.
    /// </summary>
    [TestClass]
    public class CommandServiceTests
    {
        // Mock object for the CommandParser, used to simulate its behavior in tests.
        private Mock<CommandParser> mockCommandParser;

        // Mock object for the ICanvasRenderer, used to simulate its behavior in tests.
        private Mock<ICanvasRenderer> mockCanvasRenderer;

        // The CommandService that will be tested.
        private CommandService commandService;

        /// <summary>
        /// Initializes the test environment before each test is run.
        /// This method is called before the execution of each test method.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            // Arrange phase of the tests: initializing mock objects and the object under test.
            mockCommandParser = new Mock<CommandParser>();
            mockCanvasRenderer = new Mock<ICanvasRenderer>();

            // Instantiating the CommandService with the mocked dependencies.
            commandService = new CommandService(mockCommandParser.Object, mockCanvasRenderer.Object);
        }

        /// <summary>
        /// Tests that DisplayMessage calls the DisplayTextOnCanvas method on the ICanvasRenderer interface once.
        /// </summary>
        [TestMethod]
        public void DisplayMessage_ShouldCallDisplayTextOnCanvas()
        {
            // Arrange
            string message = "Hello, Canvas!";

            // Act phase of the test: invoking the method to be tested.
            commandService.DisplayMessage(message);

            // Assert phase of the test: verifying that the method was called once with the correct parameters.
            mockCanvasRenderer.Verify(r => r.DisplayTextOnCanvas(message), Times.Once);
        }

        /// <summary>
        /// Tests that ClearCanvas calls the ClearCanvas method on the ICanvasRenderer interface once.
        /// </summary>
        [TestMethod]
        public void ClearCanvas_ShouldCallClearCanvas()
        {
            // Act
            commandService.ClearCanvas();

            // Assert
            mockCanvasRenderer.Verify(r => r.ClearCanvas(), Times.Once);
        }
    }
}
