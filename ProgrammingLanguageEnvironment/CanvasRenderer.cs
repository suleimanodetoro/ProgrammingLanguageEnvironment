using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingLanguageEnvironment
{

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
            e.Graphics.DrawImage(mainBitmap, Point.Empty);
            e.Graphics.DrawImage(cursorBitmap, Point.Empty);
        }

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

        private void DrawPointerInternal(Point position)
        {
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
                cursorGraphics.FillEllipse(Brushes.Red, position.X - POINTER_SIZE / 2, position.Y - POINTER_SIZE / 2, POINTER_SIZE, POINTER_SIZE);
            }
            canvas.Invalidate();
        }

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



        public void MoveTo(Point target)
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

        private void MoveToInternal(Point target)
        {
            currentPosition = target;
            DrawPointer(currentPosition);
        }


        public void DrawCircle(int radius, Color color, Point position, bool fill)
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




        public void DrawLine(Point startPoint, Point endPoint, Color color)
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

        private void DrawLineInternal(Point startPoint, Point endPoint, Color color)
        {
            using (var pen = new Pen(color))
            {
                graphics.DrawLine(pen, startPoint, endPoint);
            }
            canvas.Invalidate();
        }


        public void DrawRectangle(int width, int height, Color color, Point position, bool fill)
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

        public void DrawEquilateralTriangle(Point[] vertices, Color color, bool fill)
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


        public void SetPenColor(Color color)
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


        public void SetFill(bool fill)
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


