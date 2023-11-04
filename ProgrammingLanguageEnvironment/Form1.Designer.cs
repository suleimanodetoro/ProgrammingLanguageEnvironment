namespace ProgrammingLanguageEnvironment
{
    partial class CommandParserForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            multiLineCommand = new TextBox();
            singleLineInput = new TextBox();
            canvas = new PictureBox();
            runButton = new Button();
            syntaxButton = new Button();
            saveCodeButton = new Button();
            loadCodeButton = new Button();
            ((System.ComponentModel.ISupportInitialize)canvas).BeginInit();
            SuspendLayout();
            // 
            // multiLineCommand
            // 
            multiLineCommand.Location = new Point(12, 12);
            multiLineCommand.Multiline = true;
            multiLineCommand.Name = "multiLineCommand";
            multiLineCommand.PlaceholderText = "Enter Multi-Line Commands Here";
            multiLineCommand.Size = new Size(349, 302);
            multiLineCommand.TabIndex = 0;
/*            multiLineCommand.TextChanged += multiLineCommand_TextChanged;
*/            // 
            // singleLineInput
            // 
            singleLineInput.Location = new Point(12, 347);
            singleLineInput.Name = "singleLineInput";
            singleLineInput.PlaceholderText = "Enter Single Line Commands Here";
            singleLineInput.Size = new Size(349, 27);
            singleLineInput.TabIndex = 1;
            // 
            // canvas
            // 
            canvas.BackColor = SystemColors.ActiveBorder;
            canvas.Location = new Point(403, 12);
            canvas.Name = "canvas";
            canvas.Size = new Size(621, 302);
            canvas.TabIndex = 2;
            canvas.TabStop = false;
            // 
            // runButton
            // 
            runButton.Location = new Point(288, 468);
            runButton.Name = "runButton";
            runButton.Size = new Size(422, 29);
            runButton.TabIndex = 3;
            runButton.Text = "Run";
            runButton.UseVisualStyleBackColor = true;
            // 
            // syntaxButton
            // 
            syntaxButton.BackColor = SystemColors.MenuHighlight;
            syntaxButton.Location = new Point(29, 468);
            syntaxButton.Name = "syntaxButton";
            syntaxButton.Size = new Size(147, 29);
            syntaxButton.TabIndex = 4;
            syntaxButton.Text = "SYNTAX";
            syntaxButton.UseVisualStyleBackColor = false;
/*            syntaxButton.Click += button1_Click;
*/            // 
            // saveCodeButton
            // 
            saveCodeButton.Location = new Point(775, 468);
            saveCodeButton.Name = "saveCodeButton";
            saveCodeButton.Size = new Size(94, 29);
            saveCodeButton.TabIndex = 5;
            saveCodeButton.Text = "Save Code";
            saveCodeButton.UseVisualStyleBackColor = true;
/*            saveCodeButton.Click += button1_Click_1;
*/            // 
            // loadCodeButton
            // 
            loadCodeButton.Location = new Point(902, 468);
            loadCodeButton.Name = "loadCodeButton";
            loadCodeButton.Size = new Size(94, 29);
            loadCodeButton.TabIndex = 6;
            loadCodeButton.Text = "Load Code";
            loadCodeButton.UseVisualStyleBackColor = true;
/*            loadCodeButton.Click += button2_Click;
*/            // 
            // CommandParserForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1036, 568);
            Controls.Add(loadCodeButton);
            Controls.Add(saveCodeButton);
            Controls.Add(syntaxButton);
            Controls.Add(runButton);
            Controls.Add(canvas);
            Controls.Add(singleLineInput);
            Controls.Add(multiLineCommand);
            Name = "CommandParserForm";
            Text = "Command Parser Form";
/*            Load += Form1_Load;
*/            ((System.ComponentModel.ISupportInitialize)canvas).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox multiLineCommand;
        private TextBox singleLineInput;
        private PictureBox canvas;
        private Button runButton;
        private Button syntaxButton;
        private Button saveCodeButton;
        private Button loadCodeButton;
    }
}