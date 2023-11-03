using Moq;
using ProgrammingLanguageEnvironment;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestPLE
{
    [TestClass]
    public class ClearCommandTests
    {
        // Tests that the ClearCommand initializes correctly.
        // Since the ClearCommand has no parameters, we only need to check if it is created successfully.
        [TestMethod]
        public void ClearCommand_InitializesSuccessfully()
        {
            // Arrange & Act: Create a ClearCommand object. There are no parameters to arrange for this command.
            var clearCommand = new ClearCommand();

            // Assert: In this case, we simply assert that the command is not null, indicating it was created.
            Assert.IsNotNull(clearCommand);
        }

        // Tests that executing the ClearCommand calls the Clear method of the ICanvasRenderer interface.
        // It verifies that this method is called exactly once when the command is executed.
        [TestMethod]
        public void ClearCommand_Execute_CallsClearOnRenderer()
        {
            // Arrange: Create a ClearCommand and a mock object of ICanvasRenderer.
            var clearCommand = new ClearCommand();
            var mockRenderer = new Mock<ICanvasRenderer>();
            // Setup a mock expectation that the Clear method will be called.
            mockRenderer.Setup(r => r.ClearCanvas());

            // Act: Execute the clearCommand with our mock renderer as the argument.
            clearCommand.Execute(mockRenderer.Object);

            // Assert: Verify that the Clear method was called exactly one time.
            // This ensures that the Execute method of our command triggers the renderer's Clear method.
            mockRenderer.Verify(r => r.ClearCanvas(), Times.Once());
        }
    }
}
