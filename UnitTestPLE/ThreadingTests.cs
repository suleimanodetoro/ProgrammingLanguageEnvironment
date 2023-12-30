using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProgrammingLanguageEnvironment;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class ThreadingTests
    {
        private Mock<IFileService> _mockFileService;
        private Mock<ICanvasRenderer> _mockCanvasRenderer;
        private CommandService _commandService;
        private CommandParser _commandParser;

        [TestInitialize]
        public void Setup()
        {
            _mockFileService = new Mock<IFileService>();
            _mockCanvasRenderer = new Mock<ICanvasRenderer>();
            _commandParser = new CommandParser();
            _commandService = new CommandService(_commandParser, _mockCanvasRenderer.Object);
        }

        private Color DetermineColorFromCommand(string command)
        {
            // This is a simplistic example
            var parts = command.Split(' '); // e.g., "pen red"
            switch (parts[1].ToLower())
            {
                case "red":
                    return Color.Red;
                case "blue":
                    return Color.Blue;
                case "green":
                    return Color.Green;
                default:
                    throw new ArgumentException("Unknown color in command.");
            }
        }

        [TestMethod]
        public async Task ExecuteCommandsParallel_ShouldExecuteWithoutErrors()
        {
            // Arrange
            List<IEnumerable<string>> commandSets = new List<IEnumerable<string>>
            {
                new[] { "pen red", "rect 15,15" },
                new[] { "pen blue", "circle 15" },

            };

            // Act
            try
            {
                await _commandService.ExecuteCommandsParallel(commandSets);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Execution should not throw exceptions. Exception: {ex.Message}");
            }

            // Assert - Verify that the renderer was called for each command set
            _mockCanvasRenderer.Verify(renderer => renderer.DrawCircle(It.IsAny<int>(), It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()), Times.AtLeastOnce());
            _mockCanvasRenderer.Verify(renderer => renderer.DrawRectangle(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<Color>(), It.IsAny<Point>(), It.IsAny<bool>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public async Task ExecuteCommandsParallel_ShouldExecuteWithoutErrorsAndInParallel()
        {
            // Arrange
            List<IEnumerable<string>> commandSets = new List<IEnumerable<string>> {
                new[] { "pen red", "rect 15,15" },
                new[] { "pen blue", "circle 15" },
                new[] { "pen green", "tri 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "rect 15,15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
                new[] { "pen red", "circle 15" },
            };

            var stopwatch = Stopwatch.StartNew(); // To measure total execution time

            // Act
            await _commandService.ExecuteCommandsParallel(commandSets);
            stopwatch.Stop(); // Stops measuring time as soon as the awaited task is complete

            // Assert that the total execution time is less than 100 milliseconds for parallel execution
            Assert.IsTrue(stopwatch.ElapsedMilliseconds < 70, "Tasks took longer than expected, indicating they might not have executed in parallel.");

            // Assert - Verify all command sets were executed
            foreach (var commandSet in commandSets)
            {
                // Assert that SetPenColor is called with the right color
                string penCommand = commandSet.First(cmd => cmd.StartsWith("pen"));
                Color expectedColor = DetermineColorFromCommand(penCommand); // Extract the expected color from the command
                _mockCanvasRenderer.Verify(renderer => renderer.SetPenColor(expectedColor), Times.AtLeastOnce());

                // Verify each shape command
                foreach (var command in commandSet)
                {
                    if (command.StartsWith("circle"))
                    {
                        _mockCanvasRenderer.Verify(renderer => renderer.DrawCircle(It.IsAny<int>(), expectedColor, It.IsAny<Point>(), It.IsAny<bool>()), Times.AtLeastOnce());
                    }
                    else if (command.StartsWith("rect"))
                    {
                        _mockCanvasRenderer.Verify(renderer => renderer.DrawRectangle(It.IsAny<int>(), It.IsAny<int>(), expectedColor, It.IsAny<Point>(), It.IsAny<bool>()), Times.AtLeastOnce());
                    }
                    else if (command.StartsWith("tri"))
                    {
                        _mockCanvasRenderer.Verify(renderer => renderer.DrawEquilateralTriangle(It.IsAny<Point[]>(), expectedColor, It.IsAny<bool>()), Times.AtLeastOnce());
                    }
                }
            }
           
        }
    }
}
