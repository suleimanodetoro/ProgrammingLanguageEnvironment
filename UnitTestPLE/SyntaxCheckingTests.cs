
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestPLE
{
    /// <summary>
    /// Contains tests for checking the syntax parsing functionality of the CommandParser class.
    /// Ensures that exceptions are thrown and handled correctly for various erroneous inputs.
    /// </summary>
    [TestClass]
    public class SyntaxCheckingTests
    {
        // CommandParser instance used in test methods.
        private CommandParser _commandParser;

        /// <summary>
        /// Initializes a new instance of CommandParser before each test is run.
        /// </summary>

        [TestInitialize]
        public void Setup()
        {
            _commandParser = new CommandParser();
        }

        /// <summary>
        /// Tests the parsing of commands with an unknown command to ensure that the proper exception is thrown with correct line information.
        /// </summary>
        /// <remarks>
        /// The method inputs a string with an unknown command. It expects a CommandException to be thrown,
        /// and the exception message should contain information about the line number of the error.
        /// </remarks>
        /// 

        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void ParseCommands_UnknownCommand_ThrowsCommandExceptionWithLineInfo()
        {

            try
            {
                string input = "unknownCommand 10,20\nvalidCommand";
                _commandParser.ParseCommands(input);
            }
            catch (CommandException ex)
            {
                Assert.IsTrue(ex.Message.Contains("line 1"), "Error message should contain line number.");
                throw; // Rethrow to satisfy ExpectedException
            }
        }


        /// <summary>
        /// Tests parsing of commands to ensure that unclosed loops are correctly identified and reported with line information in the exception.
        /// </summary>
        /// <remarks>
        /// The method inputs a string that simulates an unclosed loop condition. It expects a CommandException to be thrown,
        /// and the exception message should contain details about the unclosed loop and the line number of the error.
        /// </remarks>
        [TestMethod]
        [ExpectedException(typeof(CommandException))]
        public void ParseCommands_UnclosedLoop_ThrowsCommandExceptionWithLineInfo()
        {
            try
            {
                string input = "while condition\nvalidCommand";
                _commandParser.ParseCommands(input);
            }
            catch (CommandException ex)
            {
                Assert.IsTrue(ex.Message.Contains("Unclosed loop"), "Error message should mention unclosed loop.");
                Assert.IsTrue(ex.Message.Contains("line 1"), "Error message should contain line number.");
                throw; // Rethrow to satisfy ExpectedException
            }
        }

    }




}
