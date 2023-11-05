using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    /// <summary>
    /// Contains unit tests for the CommandFactory class to verify its ability to create command objects from string input.
    /// </summary>
    [TestClass]
    public class CommandFactoryTests
    {

        /// <summary>
        /// Verifies that the CommandFactory creates a MoveToCommand object when provided with a valid input string for a MoveTo command.
        /// </summary>
        /// <remarks>
        /// The test checks whether the factory correctly parses the 'moveto' command and its parameters,
        /// and then creates an instance of the MoveToCommand with the appropriate properties set.
        /// </remarks>
        //Create a test to check if a command (MoveTo in this case) is correctly mapped by the command factory
        [TestMethod]
        public void CommandFactory_CreatesMoveToCommand_ForValidMoveToInput()
        {
            // Arrange: Set up any necessary variables, objects, or conditions before executing the code we want to test.
            // Here, we're creating a new instance of the CommandFactory and providing a valid input string for a MoveTo command.
            var factory = new CommandFactory();
            var validMoveToInput = "moveto 100,200";

            // Act: Perform the action we want to test.
            // We're asking the factory to create a command based on our input string, and we expect a MoveToCommand object in return.
            var command = factory.CreateCommand(validMoveToInput);

            // Assert: Verify that the method or code behaves as expected.
            // We're checking that the command isn't null (meaning something was returned),
            // and then we're verifying that the object is indeed a MoveToCommand.
            // Finally, we check that the X and Y properties of the MoveToCommand match the values we provided in the input string.
            Assert.IsNotNull(command, "The command created should not be null.");
            Assert.IsInstanceOfType(command, typeof(MoveToCommand), "The command created should be an instance of MoveToCommand.");

            // After confirming the type, we cast the command to MoveToCommand to access its properties and check the values.
            var moveToCommand = (MoveToCommand)command;
            Assert.IsNotNull(moveToCommand, "The command should be castable to a MoveToCommand object.");
            Assert.AreEqual(100, moveToCommand.TargetPosition.X, "The X coordinate of the MoveTo command should be 100.");
            Assert.AreEqual(200, moveToCommand.TargetPosition.Y, "The Y coordinate of the MoveTo command should be 200.");
        }

        /// <summary>
        /// Tests that the CommandFactory throws an InvalidCommandException when it encounters an invalid command string.
        /// </summary>
        /// <remarks>
        /// The test is designed to ensure that the factory's error handling is functioning correctly
        /// by providing an input that does not correspond to any known command type.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException), "A command with an invalid command name should throw an InvalidCommandException.")]

        public void CommandFactory_ReturnsNull_ForInvalidInput()
        {
            // Arrange
            var factory = new CommandFactory();
            var input = "invalid command";

            // Act
            var command = factory.CreateCommand(input);

            // Assert
        }

        // Additional tests may be added later...
    }
}
