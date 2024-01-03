
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace UnitTestPLE
{
    [TestClass]
    public class SyntaxCheckingTests
    {
        private CommandParser _commandParser;

        [TestInitialize]
        public void Setup()
        {
            _commandParser = new CommandParser();
        }

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
