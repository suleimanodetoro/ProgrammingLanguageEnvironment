using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using ExecutionContext = ProgrammingLanguageEnvironment.ExecutionContext;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTestPLE
{
    /// <summary>
    /// Test class to validate method functionality within the programming language environment.
    /// </summary>
    [TestClass]
    public class MethodFunctionalityTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ExecutionContext context;
        private MethodCommand methodCommand;
        private MethodCallCommand methodCallCommand;


        /// <summary>
        /// Initializes mock objects and context for each test.
        /// </summary>
        [TestInitialize]
        public void Initialize()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ExecutionContext();
            // Setup a dummy method command with no parameters and body for testing
            methodCommand = new MethodCommand("TestMethod", new List<string>(), new List<Command>());
            methodCallCommand = new MethodCallCommand("TestMethod", new List<string>());
        }

        /// <summary>
        /// Verifies that a method can be defined in the execution context and then retrieved correctly.
        /// </summary>
        [TestMethod]
        public void Method_IsCorrectlyDefinedAndRetrieved()
        {
            // Define the method in the context
            context.AddMethod("TestMethod", methodCommand);

            // Retrieve the method from the context
            var retrievedMethod = context.GetMethod("TestMethod");

            // Assertions
            Assert.IsNotNull(retrievedMethod, "The method should be successfully retrieved.");
            Assert.AreEqual(methodCommand, retrievedMethod, "The retrieved method should be the same as the one defined.");
        }


        /// <summary>
        /// Verifies that all commands within a method are executed when the method is called.
        /// </summary>
        [TestMethod]
        public void MethodCall_ExecutesAllCommands()
        {
            // Setting up a command to add to the method's body
            var moveToCommand = new MoveToCommand("10", "20");
            methodCommand.MethodBody.Add(moveToCommand);

            // Adding the method to the context
            context.AddMethod("TestMethod", methodCommand);

            // Mocking the behavior of the renderer to track DrawLine calls
            mockRenderer.Setup(r => r.MoveTo(It.IsAny<Point>())).Verifiable();

            // Executing the method call
            methodCallCommand.Execute(mockRenderer.Object, context);

            // Verifying that the MoveTo command within the method was called
            mockRenderer.Verify(r => r.MoveTo(It.IsAny<Point>()), Times.Once(), "MoveTo should be called once within the method");
        }



        /// <summary>
        /// Ensures that actual parameters provided in a method call are correctly mapped to the method's formal parameters.
        /// </summary>
        [TestMethod]
        public void MethodCall_CorrectlyMapsParameters()
        {
            // Defining a method with a single parameter and adding a command that utilizes the parameter
            var methodWithParamCommand = new MethodCommand("TestMethodWithParam", new List<string> { "p" }, new List<Command> { new MoveToCommand("p", "p") });

            // Adding method to the context
            context.AddMethod("TestMethodWithParam", methodWithParamCommand);

            // Setting up the context with the actual parameter
            context.SetVariable("a", 15);  // Let's say 15 is the value for the actual parameter

            // Creating a method call with the actual parameter
            var methodCallWithParamCommand = new MethodCallCommand("TestMethodWithParam", new List<string> { "a" });

            // Mocking the behavior of the renderer to track MoveTo calls
            mockRenderer.Setup(r => r.MoveTo(It.IsAny<Point>())).Verifiable();

            // Executing the method call
            methodCallWithParamCommand.Execute(mockRenderer.Object, context);

            // Assertions
            // Verify that MoveTo was called with the parameter value (15,15)
            mockRenderer.Verify(r => r.MoveTo(It.Is<Point>(p => p.X == 15 && p.Y == 15)), Times.Once(), "MoveTo should be called with the mapped parameter value");
        }


    }
}
