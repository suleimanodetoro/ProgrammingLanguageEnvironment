using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Interface defining the method/requirements of a canvas renderer.
    /// </summary>
    public interface ICanvasRenderer
    {
        void DrawPointer();
        void ClearCanvas();
        void MoveTo(Point target);
        void DrawCircle(int radius);
        void DrawLine(Point endPoint);
        void DrawRectangle(int width, int height);
        void DrawEquilateralTriangle(int sideLength);
        void SetPenColor(Color color);
        void SetFill(bool fill);
        void ResetPosition();
        void ExecuteCommands(List<Command> commands);
        void DisplayTextOnCanvas(string message);
        void Dispose();
    }


    /// <summary>
    /// Provides drawing capabilities on a Windows Forms PictureBox control.
    /// </summary>
    public class CanvasRenderer: ICanvasRenderer
    {
        // Fields representing the state of the CanvasRenderer
        private PictureBox canvas;
        private Graphics graphics;
        private Color currentColor = Color.Black;
        private Point currentPosition = new Point(0, 0);
        private bool fillShapes = false;
        private const int POINTER_SIZE = 5;

        // Bitmaps for the main canvas and the cursor
        private Bitmap mainBitmap;
        private Bitmap cursorBitmap;

        /// <summary>
        /// Initializes a new instance of the <see cref="CanvasRenderer"/> class.
        /// </summary>
        /// <param name="canvas">The PictureBox control to draw on.</param>
        public CanvasRenderer(PictureBox canvas)
        {
            this.canvas = canvas;
            mainBitmap = new Bitmap(canvas.Width, canvas.Height);
            cursorBitmap = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(mainBitmap);
            this.canvas.Paint += Canvas_Paint;
            DrawPointer();
        }


        /// <summary>
        /// Handles the paint event for the canvas, drawing the main bitmap and the cursor bitmap on it.
        /// </summary>
        /// <param name="sender">The source of the event, typically the canvas.</param>
        /// <param name="e">A <see cref="PaintEventArgs"/> that contains the event data.</param>
        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mainBitmap, Point.Empty);
            e.Graphics.DrawImage(cursorBitmap, Point.Empty);
        }

        /// <summary>
        /// Draws a pointer at the current position on the canvas.
        /// </summary>
        public void DrawPointer()
        {
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
                cursorGraphics.FillEllipse(Brushes.Red, currentPosition.X - POINTER_SIZE / 2, currentPosition.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Clears the canvas, resetting it to the background color.
        /// </summary>
        public void ClearCanvas()
        {
            //clear main bitmap
            graphics.Clear(canvas.BackColor);
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                //clear cursor bitmap too, making it transparent
                cursorGraphics.Clear(Color.Transparent);
            }
            //Draw a new pointer
            DrawPointer();
            //invalidates the canvas control, making sure the state is updated (main butmap clear, and pointer endered at default position0
            canvas.Invalidate();
        }

        /// <summary>
        /// Moves the current drawing position to the specified point.
        /// </summary>
        /// <param name="target">The point to move the current position to.</param>

        public void MoveTo(Point target)
        {
            currentPosition = target;
            DrawPointer();
        }

        /// <summary>
        /// Draws a circle with the specified radius at the current position.
        /// </summary>
        /// <param name="radius">The radius of the circle to draw.</param>
        public void DrawCircle(int radius)
        {
            Brush brush = new SolidBrush(currentColor);
            if (fillShapes)
            {
                graphics.FillEllipse(brush, currentPosition.X - radius, currentPosition.Y - radius, 2 * radius, 2 * radius);
            }
            else
            {
                graphics.DrawEllipse(new Pen(brush), currentPosition.X - radius, currentPosition.Y - radius, 2 * radius, 2 * radius);
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Draws a line from the current position to the specified endpoint.
        /// </summary>
        /// <param name="endPoint">The endpoint of the line to draw.</param>
        public void DrawLine(Point endPoint)
        {
            graphics.DrawLine(new Pen(currentColor), currentPosition, endPoint);
            currentPosition = endPoint;
            canvas.Invalidate();
        }

        /// <summary>
        /// Draws a rectangle with the specified width and height at the current position.
        /// </summary>
        /// <param name="width">The width of the rectangle to draw.</param>
        /// <param name="height">The height of the rectangle to draw.</param>
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
            canvas.Invalidate();
        }

        /// <summary>
        /// Draws an equilateral triangle with the specified side length at the current position.
        /// </summary>
        /// <param name="sideLength">The length of each side of the equilateral triangle.</param>
        public void DrawEquilateralTriangle(int sideLength)
        {
            double height = (Math.Sqrt(3) / 2) * sideLength;
            Point[] triangleVertices = {
                currentPosition,
                new Point(currentPosition.X + sideLength, currentPosition.Y),
                new Point(currentPosition.X + sideLength / 2, currentPosition.Y - (int)height)
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
            canvas.Invalidate();
        }

        /// <summary>
        /// Sets the current pen color for drawing.
        /// </summary>
        /// <param name="color">The color to set the pen to.</param>
        public void SetPenColor(Color color)
        {
            currentColor = color;
        }

        /// <summary>
        /// Sets whether shapes should be filled when drawn.
        /// </summary>
        /// <param name="fill">True to fill shapes, false to only draw the outline.</param>
        public void SetFill(bool fill)
        {
            fillShapes = fill;
        }

        /// <summary>
        /// Resets the current drawing position to the top-left corner of the canvas.
        /// </summary>
        public void ResetPosition()
        {
            currentPosition = new Point(0, 0);
            DrawPointer();
        }


        /// <summary>
        /// Executes a list of drawing commands on the canvas.
        /// </summary>
        /// <param name="commands">The list of commands to execute.</param>

        public void ExecuteCommands(List<Command> commands)
        {
            foreach (var command in commands)
            {
                command.Execute(this);
                DrawPointer();
            }
        }

        /// <summary>
        /// Displays a text message on the canvas.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void DisplayTextOnCanvas(string message)
        {
            using (var font = new Font("Arial", 8, FontStyle.Bold))
            {
                graphics.DrawString(message, font, Brushes.Red, new PointF(0, 5));
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Disposes of the resources used by the <see cref="CanvasRenderer"/>.
        /// </summary>
        public void Dispose()
        {
            graphics?.Dispose();
        }
    }
}


