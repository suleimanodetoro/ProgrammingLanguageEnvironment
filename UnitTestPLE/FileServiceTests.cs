using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{

    [TestClass]
    public class FileServiceTests
    {
        private FileService _fileService;

        [TestInitialize]
        public void Setup()
        {
            _fileService = new FileService();
        }

        [TestMethod]
        public void SaveCommandsToFile_ShouldSaveContentCorrectly()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName(); // Creates a temp file
            IEnumerable<string> commandsToSave = new[] { "circle 10", "tri 20" };

            // Act
            _fileService.SaveCommandsToFile(tempFilePath, commandsToSave);

            // Assert
            string[] writtenContent = File.ReadAllLines(tempFilePath);
            CollectionAssert.AreEqual(commandsToSave.ToArray(), writtenContent, "The saved content does not match the expected content.");

            // Cleanup
            File.Delete(tempFilePath); // Clean up the temp file after the test
        }

        [TestMethod]
        public void LoadCommandsFromFile_ShouldLoadContentCorrectly()
        {
            // Arrange
            string tempFilePath = Path.GetTempFileName(); // Creates a temp file
            string[] expectedCommands = { "circle 10", "tri 20" };
            File.WriteAllLines(tempFilePath, expectedCommands); // Write the expected commands to the temp file

            // Act
            IEnumerable<string> loadedCommands = _fileService.LoadCommandsFromFile(tempFilePath);

            // Assert
            CollectionAssert.AreEqual(expectedCommands, loadedCommands.ToArray(), "The loaded content does not match the expected content.");

            // Cleanup
            File.Delete(tempFilePath); // Clean up the temp file after the test
        }

        [TestMethod]
        public void SaveCommandsToFile_ShouldThrowException_WhenFilePathIsInvalid()
        {
            //test keeps failing so try catch blocks to find out why 
            // Arrange
            string invalidFilePath = "C:\\ThisDirectoryDoesNotExist\\invalidfile.txt";
            IEnumerable<string> commandsToSave = new[] { "circle 10", "tri 20" };

            try
            {
                // Act
                _fileService.SaveCommandsToFile(invalidFilePath, commandsToSave);

                // If the method does not throw, fail the test
                Assert.Fail("Expected exception was not thrown.");
            }
            catch (DirectoryNotFoundException ex)
            {
                // Expected exception was thrown, test passes
                // Log the exception details
                Console.WriteLine($"Expected DirectoryNotFoundException was thrown: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Some other exception was thrown, fail the test
                Assert.Fail($"Unexpected exception of type {ex.GetType()} was thrown.");
            }
        }
    }


}