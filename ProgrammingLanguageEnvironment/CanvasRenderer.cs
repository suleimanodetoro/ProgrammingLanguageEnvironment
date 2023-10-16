using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class CanvasRenderer
    {
        //This is a reference to the PictureBox control on the form that will be used as the drawing canvas
        private PictureBox canvas;
        // This is a Graphics object derived from the PictureBox. It provides methods to draw shapes, lines, and other graphics
        private Graphics graphics;
        // The current color( Using the system's drawing class) that will be used for drawing. Initially set to black
        private Color currentColor = Color.Black;
        // currentPosition: The current position (or coordinates) on the canvas where the next shape or line will be drawn or start from.
        private Point currentPosition;
        // fillShapes: A flag to determine if the shapes should be filled or just outlined.
        private bool fillShapes = false;
        private const int POINTER_SIZE = 5;  // Size of the pointer


        // When a new CanvasRenderer object is created, it's initialized with a reference to a PictureBox control. The Graphics object is then derived from this PictureBox.
        public CanvasRenderer(PictureBox canvas)
        {
            this.canvas = canvas;
            this.graphics = canvas.CreateGraphics();
        }

        //MoveTo render tool
        public void MoveTo(Point target)
        {
            currentPosition = target;
        }

        public void DrawCircle(int radius)
        {
            Brush brush = new SolidBrush(currentColor);
            //implement filled shapes requirement
            if (fillShapes)
            {
                graphics.FillEllipse(brush, currentPosition.X - radius, currentPosition.Y - radius, 2 * radius, 2 * radius);
            } else
            {
                graphics.DrawEllipse(new Pen(brush), currentPosition.X - radius, currentPosition.Y - radius, 2 * radius, 2 * radius);
            }
        }

        public void SetPenColor (Color color)
        {
            currentColor = color;
        }

        public void SetFill ( bool fill)
        {
            fillShapes = fill;
        }

        public void Clear()
        {
            graphics.Clear(canvas.BackColor);
        }

        public void ExecuteCommands( List<ICommand> commands)
        {
            foreach (var command in commands) 
            {
                //For each command, clear the pointer (so we can move it)
                ClearPointer();
                //execute method of each command object is called
                command.Execute(this);
                DrawPointer();

            }
        }
        public void DrawPointer()
        {
            graphics.FillEllipse(Brushes.Red, currentPosition.X - POINTER_SIZE / 2, currentPosition.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
        }

        public void ClearPointer()
        {
            graphics.FillEllipse(new SolidBrush(canvas.BackColor), currentPosition.X - POINTER_SIZE / 2, currentPosition.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
        }

        public void Dispose ()
        {
            graphics?.Dispose();
        }

    }
}
