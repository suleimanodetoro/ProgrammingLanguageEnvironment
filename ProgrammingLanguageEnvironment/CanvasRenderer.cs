using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingLanguageEnvironment
{
    /// <summary>
    /// Defines functionality for rendering graphics onto a canvas.
    /// </summary>
    public interface ICanvasRenderer
    {
        void DrawPointer(Point position);
        void ClearCanvas();
        void MoveTo(Point target);
        void DrawCircle(int radius, Color color, Point position, bool fill);
        void DrawLine(Point startPoint, Point endPoint, Color color);
        void DrawRectangle(int width, int height, Color color, Point position, bool fill);
        void DrawEquilateralTriangle(Point[] vertices, Color color, bool fill);
        void SetPenColor(Color color);
        void SetFill(bool fill);
        void ResetPosition();
        void ExecuteCommands(List<Command> commands);
        void DisplayTextOnCanvas(string message);
        void Dispose();
    }

    /// <summary>
    /// Provides functionality for rendering shapes and text onto a canvas.
    /// </summary>
    public class CanvasRenderer : ICanvasRenderer
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

        private readonly object renderLock = new object();


        /// <summary>
        /// Initializes a new instance of the CanvasRenderer class.
        /// </summary>
        /// <param name="canvas">The PictureBox control used as a drawing canvas.</param>

        public CanvasRenderer(PictureBox canvas)
        {
            this.canvas = canvas;
            mainBitmap = new Bitmap(canvas.Width, canvas.Height);
            cursorBitmap = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(mainBitmap);
            this.canvas.Paint += Canvas_Paint;
            currentPosition = new Point(0, 0);
            DrawPointer(currentPosition);
        }


 
        private void Canvas_Paint(object? sender, PaintEventArgs e)
        {
            // Renders the main and cursor bitmaps onto the canvas.
            e.Graphics.DrawImage(mainBitmap, Point.Empty);
            e.Graphics.DrawImage(cursorBitmap, Point.Empty);
        }

        /// <summary>
        /// Draws a pointer at the specified position on the canvas.
        /// </summary>
        /// <param name="position">The position to place the pointer.</param>

        public void DrawPointer(Point position)
        {
            if (canvas.InvokeRequired)
            {
                canvas.Invoke(new Action(() => DrawPointerInternal(position)));
            }
            else
            {
                DrawPointerInternal(position);
            }
        }

        /// <summary>
        /// Draws a pointer at the specified position on the cursor layer.
        /// </summary>
        /// <param name="position">The position to draw the pointer.</param>
        private void DrawPointerInternal(Point position)
        {
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
                cursorGraphics.FillEllipse(Brushes.Red, position.X - POINTER_SIZE / 2, position.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Clears all graphics from the canvas.
        /// </summary>
        public void ClearCanvas()
        {
            if (canvas.InvokeRequired)
            {
                canvas.Invoke(new Action(ClearCanvasInternal));
            }
            else
            {
                ClearCanvasInternal();
            }
        }

        /// <summary>
        /// Clears the canvas of all drawings.
        /// </summary>
        public void ClearCanvasInternal()
        {
            graphics.Clear(canvas.BackColor);
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
            }
            DrawPointer(currentPosition);
            canvas.Invalidate();
        }


        /// <summary>
        /// Moves the drawing pointer to the specified target location.
        /// </summary>
        /// <param name="target">The target position to move the pointer to.</param>

        public void MoveTo(Point target)
        {
            lock (renderLock)  // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => MoveToInternal(target)));
                }
                else
                {
                    MoveToInternal(target);
                }
            }
        }




        /// <summary>
        /// Moves the current position of the drawing pointer to the specified target location.
        /// </summary>
        /// <param name="target">The target position to move the pointer to.</param>
        private void MoveToInternal(Point target)
        {
            currentPosition = target;
            DrawPointer(currentPosition);  // This already checks for InvokeRequired internally
        }

        /// <summary>
        /// Draws a circle with the specified parameters.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="color">The color of the circle.</param>
        /// <param name="position">The position to draw the circle.</param>
        /// <param name="fill">Whether the circle is filled.</param>
        public void DrawCircle(int radius, Color color, Point position, bool fill)
        {
            lock (renderLock)  // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => DrawCircleInternal(radius, color, position, fill)));
                }
                else
                {
                    DrawCircleInternal(radius, color, position, fill);
                }
            }
        }


        /// <summary>
        /// Draws a circle on the canvas.
        /// </summary>
        /// <param name="radius">The radius of the circle.</param>
        /// <param name="color">The color of the circle.</param>
        /// <param name="position">The center position of the circle.</param>
        /// <param name="fill">Whether the circle is filled.</param>
        private void DrawCircleInternal(int radius, Color color, Point position, bool fill)
        {
            Brush brush = new SolidBrush(color);
            if (fill)
            {
                graphics.FillEllipse(brush, position.X - radius, position.Y - radius, 2 * radius, 2 * radius);
            }
            else
            {
                graphics.DrawEllipse(new Pen(brush), position.X - radius, position.Y - radius, 2 * radius, 2 * radius);
            }
            canvas.Invalidate();
        }



        /// <summary>
        /// Draws a line between two points with the specified color.
        /// </summary>
        /// <param name="startPoint">The starting point of the line.</param>
        /// <param name="endPoint">The ending point of the line.</param>
        /// <param name="color">The color of the line.</param>
        public void DrawLine(Point startPoint, Point endPoint, Color color)
        {
            lock (renderLock)  // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => DrawLineInternal(startPoint, endPoint, color)));
                }
                else
                {
                    DrawLineInternal(startPoint, endPoint, color);
                }
            }
        }
        

        /// <summary>
        /// Draws a line between two points.
        /// </summary>
        /// <param name="startPoint">The starting point of the line.</param>
        /// <param name="endPoint">The ending point of the line.</param>
        /// <param name="color">The color of the line.</param>
        private void DrawLineInternal(Point startPoint, Point endPoint, Color color)
        {
            using (var pen = new Pen(color))
            {
                graphics.DrawLine(pen, startPoint, endPoint);
            }
            canvas.Invalidate();
        }


        /// <summary>
        /// Draws a rectangle with the specified width, height, color, position, and fill status.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="position">Position to place the rectangle.</param>
        /// <param name="fill">Whether the rectangle is filled.</param>

        public void DrawRectangle(int width, int height, Color color, Point position, bool fill)
        {
            lock (renderLock) // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => DrawRectangleInternal(width, height, color, position, fill)));
                }
                else
                {
                    DrawRectangleInternal(width, height, color, position, fill);
                }
            }
        }

        /// <summary>
        /// Draws a rectangle on the canvas.
        /// </summary>
        /// <param name="width">Width of the rectangle.</param>
        /// <param name="height">Height of the rectangle.</param>
        /// <param name="color">Color of the rectangle.</param>
        /// <param name="position">Top-left position of the rectangle.</param>
        /// <param name="fill">Whether the rectangle is filled.</param>
        private void DrawRectangleInternal(int width, int height, Color color, Point position, bool fill)
        {
            Brush brush = new SolidBrush(color);
            if (fill)
            {
                graphics.FillRectangle(brush, position.X, position.Y, width, height);
            }

            else
            {
                graphics.DrawRectangle(new Pen(brush), position.X, position.Y, width, height);
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Draws an equilateral triangle with specified vertices, color, and fill status.
        /// </summary>
        /// <param name="vertices">The vertices of the triangle.</param>
        /// <param name="color">The color of the triangle.</param>
        /// <param name="fill">Whether the triangle is filled.</param>
        public void DrawEquilateralTriangle(Point[] vertices, Color color, bool fill)
        {
            lock (renderLock) // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => DrawEquilateralTriangleInternal(vertices, color, fill)));
                }
                else
                {
                    DrawEquilateralTriangleInternal(vertices, color, fill);
                }
            }
        }

        /// <summary>
        /// Draws an equilateral triangle on the canvas.
        /// </summary>
        /// <param name="vertices">The vertices of the triangle.</param>
        /// <param name="color">The color of the triangle.</param>
        /// <param name="fill">Whether the triangle is filled.</param>
        private void DrawEquilateralTriangleInternal(Point[] vertices, Color color, bool fill)
        {
            Brush brush = new SolidBrush(color);
            if (fill)
            {
                graphics.FillPolygon(brush, vertices);
            }
            else
            {
                graphics.DrawPolygon(new Pen(brush), vertices);
            }
            canvas.Invalidate();
        }

        /// <summary>
        /// Sets the current pen color to the specified color.
        /// </summary>
        /// <param name="color">The color to set the pen.</param>
        public void SetPenColor(Color color)
        {
            lock (renderLock)  // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => currentColor = color));
                }
                else
                {
                    currentColor = color;
                }
            }
        }


        /// <summary>
        /// Sets whether shapes are filled when drawn.
        /// </summary>
        /// <param name="fill">True to fill shapes, false otherwise.</param>
        public void SetFill(bool fill)
        {
            lock (renderLock)  // Locking to ensure thread-safe access
            {
                if (canvas.InvokeRequired)
                {
                    canvas.Invoke(new Action(() => fillShapes = fill));
                }
                else
                {
                    fillShapes = fill;
                }
            }
        }


        /// <summary>
        /// Resets the current position of the drawing pointer to the top-left corner of the canvas.
        /// </summary>
        public void ResetPosition()
        {
            if (canvas.InvokeRequired)
            {
                canvas.Invoke(new Action(() => {
                    currentPosition = new Point(0, 0);
                    DrawPointer(currentPosition);
                }));
            }
            else
            {
                currentPosition = new Point(0, 0);
                DrawPointer(currentPosition);
            }
        }

        /// <summary>
        /// Executes a list of commands, rendering the appropriate graphics.
        /// </summary>
        /// <param name="commands">The commands to execute.</param>
        public void ExecuteCommands(List<Command> commands)
        {
            ExecutionContext context = new ExecutionContext();

            foreach (var command in commands)
            {
                command.Execute(this, context);
                DrawPointer(context.CurrentPosition);
            }
        }

        /// <summary>
        /// Displays a text message on the canvas.
        /// </summary>
        /// <param name="message">The message to display.</param>
        public void DisplayTextOnCanvas(string message)
        {
            using (var font = new Font("Arial", 5, FontStyle.Bold))
            {
                graphics.DrawString(message, font, Brushes.Red, new PointF(0, 0));
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


