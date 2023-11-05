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
    /// <summary>
    /// Contains unit tests for the ClearCommand class to verify its functionality.
    /// </summary>        
    [TestClass]
    public class ClearCommandTests
    {
       
        /// <summary>
        /// Verifies that the ClearCommand object initializes correctly.
        /// </summary>
        /// <remarks>
        /// Since ClearCommand does not take any parameters upon initialization, the test
        /// simply checks for the creation of the object.
        /// </remarks>
        [TestMethod]
        public void ClearCommand_InitializesSuccessfully()
        {
            // Arrange & Act: Create a ClearCommand object. There are no parameters to arrange for this command.
            var clearCommand = new ClearCommand();

            // Assert: In this case, we simply assert that the command is not null, indicating it was created.
            Assert.IsNotNull(clearCommand);
        }

        /// <summary>
        /// Tests that executing the ClearCommand results in a call to the ClearCanvas method
        /// on an object implementing the ICanvasRenderer interface.
        /// </summary>
        /// <remarks>
        /// This test confirms that the ClearCanvas method is invoked exactly once when
        /// the ClearCommand's Execute method is called, ensuring the command's effect is
        /// propagated to the renderer.
        /// </remarks>
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
