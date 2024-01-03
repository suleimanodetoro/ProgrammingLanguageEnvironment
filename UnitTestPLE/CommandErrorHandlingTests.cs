using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingLanguageEnvironment;
using System;

namespace UnitTestPLE
{
    [TestClass]
    public class CommandExceptionTests
    {
        private CommandFactory _commandFactory;

        [TestInitialize]
        public void Setup()
        {
            // Initialize CommandFactory before each test
            _commandFactory = new CommandFactory();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidCommandException))]
        public void CreateCommand_WithUnknownCommand_ThrowsInvalidCommandException()
        {
            // Act
            _commandFactory.CreateCommand("unknowncommand 10");
        }

       

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidPenParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("pen"); // Missing color
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidFillParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("fill maybe"); // Invalid fill parameter
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidResetParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("reset 123"); // Reset does not take parameters
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidClearParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("clear 123"); // Clear does not take parameters
        }
    }
}
