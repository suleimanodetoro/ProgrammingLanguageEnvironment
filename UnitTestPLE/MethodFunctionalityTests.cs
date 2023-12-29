using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProgrammingLanguageEnvironment;
using ExecutionContext = ProgrammingLanguageEnvironment.ExecutionContext;
using System.Collections.Generic;
using System.Drawing;

namespace UnitTestPLE
{
    [TestClass]
    public class MethodFunctionalityTests
    {
        private Mock<ICanvasRenderer> mockRenderer;
        private ExecutionContext context;
        private MethodCommand methodCommand;
        private MethodCallCommand methodCallCommand;

        [TestInitialize]
        public void Initialize()
        {
            mockRenderer = new Mock<ICanvasRenderer>();
            context = new ExecutionContext();
            // Assuming "TestMethod" is the name of the method and it takes no parameters
            methodCommand = new MethodCommand("TestMethod", new List<string>(), new List<Command>());
            methodCallCommand = new MethodCallCommand("TestMethod", new List<string>());
        }
        //This test verifies that a method can be defined and then retrieved correctly.
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


        //The following test ensures that when a method is called, all the commands within that method are executed.
        [TestMethod]
        public void MethodCall_ExecutesAllCommands()
        {
            // Setting up a command to add to the method
            var moveToCommand = new MoveToCommand("10", "20");
            methodCommand.MethodBody.Add(moveToCommand);

            // Adding the method to the context
            context.AddMethod("TestMethod", methodCommand);

            // Mocking the behavior of the renderer to track DrawLine calls
            mockRenderer.Setup(r => r.MoveTo(It.IsAny<Point>())).Verifiable();

            // Executing the method call
            methodCallCommand.Execute(mockRenderer.Object, context);

            // Verifying the MoveTo command within the method was called
            mockRenderer.Verify(r => r.MoveTo(It.IsAny<Point>()), Times.Once(), "MoveTo should be called once within the method");
        }
        
        
        
        /*
         The following test will ensure that actual parameters provided in a method call are correctly mapped to the method's formal parameters.
        */

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
