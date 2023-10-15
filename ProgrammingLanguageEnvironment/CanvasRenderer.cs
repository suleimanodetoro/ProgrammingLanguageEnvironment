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

    }
}
