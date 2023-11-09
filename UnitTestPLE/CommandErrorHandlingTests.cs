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
        public void CreateCommand_WithInvalidMoveToParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("moveto 10"); // Missing y coordinate
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidDrawToParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("drawto x,y"); // Invalid numbers
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
        public void CreateCommand_WithInvalidRectParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("rect 10"); // Missing height
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidCircleParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("circle notanumber"); // Invalid radius
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidParameterException))]
        public void CreateCommand_WithInvalidTriParameters_ThrowsInvalidParameterException()
        {
            // Act
            _commandFactory.CreateCommand("tri notanumber"); // Invalid side length
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
