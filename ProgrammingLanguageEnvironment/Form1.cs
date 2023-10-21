namespace ProgrammingLanguageEnvironment
{
    public partial class CommandParserForm : Form
    {

        private CommandParser commandParser;
        private CanvasRenderer canvasRenderer;
        public CommandParserForm()
        {
            InitializeComponent();
            //On Run Button Press event, execute function
            runButton.Click += RunButton_Click;
            
            //create new instance of Command Parser
            commandParser = new CommandParser();
            // create new instance of canvas renderer
            canvasRenderer = new CanvasRenderer(canvas);


        }

        private void RunButton_Click(object? sender, EventArgs e)
        {
            //store command strings from multi line and single line input
            string commands = multiLineCommand.Text + Environment.NewLine + singleLineInput.Text;

            // store parsed commands
            var parsedCommands = commandParser.ParseCommands(commands);

            //log parsed commands
            foreach(var cmd in parsedCommands)
            {
                Console.WriteLine(cmd.ToString());
            }

            canvasRenderer.ExecuteCommands(parsedCommands);
        

        }

        //include breakpoint to verify if this works, CAUSE FIX DID NOT WORK!!!
        private void Form1_Load(object sender, EventArgs e)
        {
      

        }

       
    }
}