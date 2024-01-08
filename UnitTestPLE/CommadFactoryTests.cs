using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingLanguageEnvironment;
using System;

namespace UnitTestPLE
{
    /// <summary>
    /// Test class for CommandFactory.
    /// </summary>
    [TestClass]
    public class CommandFactoryTests
    {
        private CommandFactory factory;

        /// <summary>
        /// Setup method for tests, initializes CommandFactory.
        /// </summary>
        [TestInitialize]
        public void Setup()
        {
            factory = new CommandFactory();
        }

        /// <summary>
        /// Test to ensure a MoveToCommand is created for valid input.
        /// </summary>
        [TestMethod]
        public void CommandFactory_CreatesMoveToCommand_ForValidMoveToInput()
        {
            // Arrange
            string validMoveToInput = "moveto 10,20";

            // Act
            var command = factory.CreateCommand(validMoveToInput);

            // Assert
            Assert.IsNotNull(command, "The command created should not be null.");
            Assert.IsInstanceOfType(command, typeof(MoveToCommand), "The command created should be an instance of MoveToCommand.");
        }

        /// <summary>
        /// Test to ensure a DrawToCommand is created for valid input.
        /// </summary>
        [TestMethod]
        public void CommandFactory_CreatesDrawToCommand_ForValidDrawToInput()
        {
            // Arrange
            string validDrawToInput = "drawto 30,40";

            // Act
            var command = factory.CreateCommand(validDrawToInput);

            // Assert
            Assert.IsNotNull(command, "The command created should not be null.");
            Assert.IsInstanceOfType(command, typeof(DrawToCommand), "The command created should be an instance of DrawToCommand.");
        }

        /// <summary>
        /// Test to ensure a CircleCommand is created for valid input.
        /// </summary>
        [TestMethod]
        public void CommandFactory_CreatesCircleCommand_ForValidCircleInput()
        {
            // Arrange
            string validCircleInput = "circle 50";

            // Act
            var command = factory.CreateCommand(validCircleInput);

            // Assert
            Assert.IsNotNull(command, "The command created should not be null.");
            Assert.IsInstanceOfType(command, typeof(CircleCommand), "The command created should be an instance of CircleCommand.");
        }

        /// <summary>
        /// Test to ensure a RectangleCommand is created for valid input.
        /// </summary>
        [TestMethod]
        public void CommandFactory_CreatesRectCommand_ForValidRectInput()
        {
            // Arrange
            string validRectInput = "rect 60,70";

            // Act
            var command = factory.CreateCommand(validRectInput);

            // Assert
            Assert.IsNotNull(command, "The command created should not be null.");
            Assert.IsInstanceOfType(command, typeof(RectangleCommand), "The command created should be an instance of RectangleCommand.");
        }

        /// <summary>
        /// Test to ensure an exception is thrown for invalid input.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void CommandFactory_ThrowsException_ForInvalidInput()
        {
            // Arrange
            string invalidInput = "notacommand 123";

            // Act
            var command = factory.CreateCommand(invalidInput);

            // Assert is handled by ExpectedException
        }
    }
}
