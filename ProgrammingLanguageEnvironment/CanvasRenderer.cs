using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProgrammingLanguageEnvironment
{
    public class CanvasRenderer
    {
        private PictureBox canvas;
        private Graphics graphics;
        private Color currentColor = Color.Black;
        private Point currentPosition = new Point(0, 0);
        private bool fillShapes = false;
        private const int POINTER_SIZE = 5;

        private Bitmap mainBitmap;
        private Bitmap cursorBitmap;

        public CanvasRenderer(PictureBox canvas)
        {
            this.canvas = canvas;
            mainBitmap = new Bitmap(canvas.Width, canvas.Height);
            cursorBitmap = new Bitmap(canvas.Width, canvas.Height);
            graphics = Graphics.FromImage(mainBitmap);
            this.canvas.Paint += Canvas_Paint;
            DrawPointer();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawImage(mainBitmap, Point.Empty);
            e.Graphics.DrawImage(cursorBitmap, Point.Empty);
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

        public void ClearCanvas()
        {
            graphics.Clear(canvas.BackColor);
            using (Graphics cursorGraphics = Graphics.FromImage(cursorBitmap))
            {
                cursorGraphics.Clear(Color.Transparent);
            }
            DrawPointer();
            canvas.Invalidate();
        }

        public void MoveTo(Point target)
        {
            currentPosition = target;
            DrawPointer();
        }

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

        public void DrawLine(Point endPoint)
        {
            graphics.DrawLine(new Pen(currentColor), currentPosition, endPoint);
            currentPosition = endPoint;
            canvas.Invalidate();
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
            canvas.Invalidate();
        }

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

        public void SetPenColor(Color color)
        {
            currentColor = color;
        }

        public void SetFill(bool fill)
        {
            fillShapes = fill;
        }

        public void ResetPosition()
        {
            currentPosition = new Point(0, 0);
            DrawPointer();
        }

        public void ExecuteCommands(List<Command> commands)
        {
            foreach (var command in commands)
            {
                command.Execute(this);
                DrawPointer();
            }
        }

        public void Dispose()
        {
            graphics?.Dispose();
        }
    }
}
