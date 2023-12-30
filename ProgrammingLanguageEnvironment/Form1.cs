namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// this class represents the main form for the Command Parser application.
    /// </summary>
    public partial class CommandParserForm : Form
    {
        private readonly ICommandService _commandService;
        private readonly IFileService _fileService;
        private List<IEnumerable<string>> _loadedCommands = new List<IEnumerable<string>>();
        private readonly CommandParser _commandParser; 
        private readonly ICanvasRenderer _canvasRenderer; 

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
        /// Handles the Run button click event to execute commands.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
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
        /// Handles the Syntax Check button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
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
        /// Handles the Save button click event to save commands to a file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        private void saveBtn_Click(object sender, EventArgs e)
        {
            SaveCommands();

        }

        /// <summary>
        /// Handles the Load button click event to load commands from a file.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
        // Event handler for the 'Load' button click
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

        // Method to save commands to a file. The defuatl file name will be commands.txt
        /// <summary>
        /// Saves the current commands to a file.
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


        // Method to retrieve commands from the input fields (i.e The multiline TextBox in my app)
        private IEnumerable<string> GetCommandsFromInput()
        {
            // Check the multiline TextBox for command input
            return multiLineCommand.Lines;
        }

        private void multiLineCommand_TextChanged(object sender, EventArgs e)
        {

        }

        private void loadCodeButton_Click(object sender, EventArgs e)
        {

        }

        private async void runParallelButton_Click(object sender, EventArgs e)
        {
            await Console.Out.WriteLineAsync("This button is working");
            await ExecuteCommandsInParallel(_loadedCommands);

        }
    }
}