using ProgrammingLanguageEnvironment;

namespace UnitTestPLE
{
    /// <summary>
    /// Contains unit tests for the CommandFactory class to verify its ability to create command objects from string input.
    /// </summary>
    [TestClass]
    public class CommandParserTests
    {
        
        private CommandParser _commandParser;

        [TestInitialize]
        public void Initialize()
        {
            _commandParser = new CommandParser();
        }


        /// <summary>
        /// Verifies that the CommandFactory creates a MoveToCommand object when provided with a valid input string for a MoveTo command.
        /// </summary>
        /// <remarks>
        /// The test checks whether the factory correctly parses the 'moveto' command and its parameters,
        /// and then creates an instance of the MoveToCommand with the appropriate properties set.
        /// </remarks>

        [TestMethod]
        public void ParseCommands_ValidCommand_ReturnsCommandList()
        {
            // Arrange
            string input = "moveto 10,20";

            // Act
            var commands = _commandParser.ParseCommands(input);

            // Assert
            Assert.IsNotNull(commands);
            Assert.IsTrue(commands.Count > 0); // This ensures that commands list is not empty
            Assert.IsInstanceOfType(commands.First(), typeof(MoveToCommand)); // Ensure that the first command is a MoveToCommand
        }


        /// <summary>
        /// Tests that the CommandFactory throws an InvalidCommandException when it encounters an invalid command string.
        /// </summary>
        /// <remarks>
        /// The test is designed to ensure that the factory's error handling is functioning correctly
        /// by providing an input that does not correspond to any known command type.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void ParseCommands_UnknownCommand_ThrowsInvalidCommandException()
        {
            // Arrange
            string input = "unknownCommand 10,20";

            // Act
            _commandParser.ParseCommands(input); // Exception is expected to be thrown here

        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void ParseCommands_InvalidParameters_ThrowsInvalidParameterException()
        {
            // Arrange
            string input = "moveto 90";

            // Act
            _commandParser.ParseCommands(input); // Exception is expected to be thrown here

        }

        // note to self, more tests here later. ahhhhh :(
    }
}