namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Represents the main form for the Command Parser application, handling user interactions and command processing.
    /// </summary>
    public partial class CommandParserForm : Form
    {
        private readonly ICommandService _commandService;
        private readonly IFileService _fileService;
        private List<IEnumerable<string>> _loadedCommands = new List<IEnumerable<string>>();
        private readonly CommandParser _commandParser; 
        private readonly ICanvasRenderer _canvasRenderer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParserForm"/> class.
        /// Sets up the command parser, renderer, and event handlers for user interactions.
        /// </summary>
        public CommandParserForm()
        {
            InitializeComponent();

            // Assign to class fields instead of creating local variables
            _commandParser = new CommandParser();
            _canvasRenderer = new CanvasRenderer(canvas);

            // Instantiate the command service with the parser and renderer
            _commandService = new CommandService(_commandParser, _canvasRenderer);

            // Instantiate the file service
            _fileService = new FileService();

            // Hook up the event handlers
            this.saveCodeButton.Click += new System.EventHandler(this.saveBtn_Click);
            this.runButton.Click += new EventHandler(this.RunButton_Click);
            this.syntaxButton.Click += new EventHandler(this.SyntaxButton_Click);
            this.loadCodeButton.Click += new EventHandler(this.loadBtn_Click);
        }

        /// <summary>
        /// Executes multiple sets of commands in parallel.
        /// </summary>
        /// <param name="commandLists">A list of command sets, where each set represents a sequence of commands.</param>
        /// <returns>A task that represents the asynchronous operation of executing commands in parallel.</returns>
        private async Task ExecuteCommandsInParallel(List<IEnumerable<string>> commandLists)
        {
            var tasks = commandLists.Select(commands =>
            {
                return Task.Run(() =>
                {
                    ExecutionContext context = new ExecutionContext();
                    var parsedCommands = _commandParser.ParseCommands(string.Join(Environment.NewLine, commands));
                    foreach (var command in parsedCommands)
                    {
                        // Access to _canvasRenderer might need synchronization for thread safety
                        command.Execute(_canvasRenderer, context);
                    }
                });
            });

            await Task.WhenAll(tasks);
        }


        /// <summary>
        /// Executes commands when the 'Run' button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void RunButton_Click(object? sender, EventArgs e)
        {
            // Delegate command execution to CommandService
            string commands = multiLineCommand.Text + Environment.NewLine + singleLineInput.Text;
            try
            {
                _commandService.ExecuteCommands(commands);
            }
            catch (Exception ex)
            {
                // Handle the error, e.g., display a message on your canvas or in a message box
                // Syntax is not correct so display the error message
                _commandService.ClearCanvas();
                _commandService.DisplayMessage(ex.Message);
            }
        }


        /// <summary>
        /// Checks the syntax of the provided commands when the 'Syntax Check' button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void SyntaxButton_Click(object? sender, EventArgs e)
        {
            _commandService.ClearCanvas();
            // Delegate syntax checking to CommandService
            string commands = multiLineCommand.Text + Environment.NewLine + singleLineInput.Text;
            string errorMessage;
            bool isSyntaxCorrect = _commandService.CheckSyntax(commands, out errorMessage);

            if (isSyntaxCorrect)
            {
                // Display "Syntax is okay!" on the canvas
                _commandService.DisplayMessage("Syntax is okay!");
            }
            else
            {
                // Syntax is not correct so display the error message
                _commandService.DisplayMessage(errorMessage);
            }
        }



        /// <summary>
        /// Saves the current commands to a file when the 'Save' button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveCommands();

        }

        /// <summary>
        /// Loads commands from a file when the 'Load' button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private void loadBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Multiselect = true,
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*"
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                _loadedCommands.Clear(); // Clear previous commands

                foreach (var filePath in openFileDialog.FileNames)
                {
                    try
                    {
                        var commands = _fileService.LoadCommandsFromFile(filePath);
                        _loadedCommands.Add(commands);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        /// <summary>
        /// Saves the currently entered commands to a file.
        /// </summary>
        private void SaveCommands()
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                FileName = "commands.txt", // Default file name but can be changed by user
                Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*" // Filter files by extension
            };

            // Show save file dialog box
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Get the path of specified file
                string filePath = saveFileDialog.FileName;

                try
                {
                    // Call the FileService to save the commands to the file
                    _fileService.SaveCommandsToFile(filePath, GetCommandsFromInput());
                }
                catch (Exception ex)
                {
                    // Show an error message if something goes wrong
                    MessageBox.Show("Error saving file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        /// <summary>
        /// Retrieves commands from the input fields.
        /// </summary>
        /// <returns>An enumerable collection of strings representing commands.</returns>
        private IEnumerable<string> GetCommandsFromInput()
        {
            // Check the multiline TextBox for command input
            return multiLineCommand.Lines;
        }

        /// <summary>
        ///Event listener for multiline text field change
        /// </summary>
        private void multiLineCommand_TextChanged(object sender, EventArgs e)
        {

        }

        

        /// <summary>
        /// Executes commands in parallel when the 'Run Parallel' button is clicked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">Event data.</param>
        private async void runParallelButton_Click(object sender, EventArgs e)
        {
            await Console.Out.WriteLineAsync("This button is working");
            await ExecuteCommandsInParallel(_loadedCommands);

        }
    }
}