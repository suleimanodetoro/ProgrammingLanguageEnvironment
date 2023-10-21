using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
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
        private Point currentPosition = new Point(0,0); //Initial position of the point
        // fillShapes: A flag to determine if the shapes should be filled or just outlined.
        private bool fillShapes = false;
        private const int POINTER_SIZE = 5;  // Size of the pointer

        private Bitmap mainBitmap;
        private Bitmap cursorBitmap;

        // When a new CanvasRenderer object is created, it's initialized with a reference to a PictureBox control. The Graphics object is then derived from this PictureBox.
        public CanvasRenderer(PictureBox canvas)
        {
            this.canvas = canvas;
            this.mainBitmap = new Bitmap(canvas.Width, canvas.Height);
            this.cursorBitmap = new Bitmap(canvas.Width, canvas.Height);
            this.graphics = Graphics.FromImage(mainBitmap);
            canvas.Paint += Canvas_Paint;
            DrawPointer();
            /* DrawPointer method call has been removed, trying shown event for form handler to see if it fixes pointer not showing initially*/
        }

        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mainBitmap, 0, 0);
            e.Graphics.DrawImage(cursorBitmap, 0, 0);
        }

        //MoveTo render tool
        public void MoveTo(Point target)
        {
            currentPosition = target;
            DrawPointer();
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

        public void DrawLine(Point endPoint)
        {
            graphics.DrawLine(new Pen(currentColor), currentPosition, endPoint);
            currentPosition = endPoint;  // Update the current point to the end of the line
        }

        public void DrawRectangle(int width, int height)
        {
            Brush brush = new SolidBrush(currentColor);
            if (fillShapes)
            {
                graphics.FillRectangle(brush, currentPosition.X, currentPosition.Y, width, height);
            }
            else
            {
                graphics.DrawRectangle(new Pen(brush), currentPosition.X, currentPosition.Y, width, height);
            }
        }

        public void DrawTriangle(int side)
        {
            Point[] triangleVertices = {
            currentPosition,
            new Point(currentPosition.X + side, currentPosition.Y),
            new Point(currentPosition.X + side / 2, currentPosition.Y - (int)(Math.Sqrt(3) / 2 * side))
        };
            Brush brush = new SolidBrush(currentColor);
            if (fillShapes)
            {
                graphics.FillPolygon(brush, triangleVertices);
            }
            else
            {
                graphics.DrawPolygon(new Pen(brush), triangleVertices);
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

        public void ClearCanvas()
        {
            // Clear the main bitmap
            graphics.Clear(canvas.BackColor); // same colour as before being cleared

            // Clear the cursor bitmap (though I'll redraw the pointer immediately after)
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
            }

            // Redraw the pointer
            DrawPointer();

            // Refresh the canvas
            canvas.Invalidate();
        }


        public void ExecuteCommands(List<ICommand> commands)
        {
            foreach (var command in commands)
            {
                command.Execute(this);
                DrawPointer();
            }
        }
        public void DrawPointer()
        {
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
                cursorGraphics.FillEllipse(Brushes.Red, currentPosition.X - POINTER_SIZE / 2, currentPosition.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
            }
            canvas.Invalidate();
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
