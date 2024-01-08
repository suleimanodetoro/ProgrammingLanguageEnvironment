namespace ProgrammingLanguageEnvironment
{
    public interface ICommandService
    {
        void ExecuteCommands(string commands, ExecutionContext context);
        Task ExecuteCommandsParallel(IEnumerable<IEnumerable<string>> commandSets);
        bool CheckSyntax(string commands, out string errorMessage);
        void DisplayMessage(string message);
        void ClearCanvas();
    }
}