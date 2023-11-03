using ProgrammingLanguageEnvironment;

namespace UnitTestPLE
{
    [TestClass]
    public class CommandParserTests
    {
        private CommandParser _commandParser;

        [TestInitialize]
        public void Initialize()
        {
            _commandParser = new CommandParser();
        }


        //Test for valid command (moveto for example)

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


        //Test for unknown command 

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
            string input = "moveto invalidParam";

            // Act
            _commandParser.ParseCommands(input); // Exception is expected to be thrown here

        }

        // note to self, more tests here later. ahhhhh :(
    }
}