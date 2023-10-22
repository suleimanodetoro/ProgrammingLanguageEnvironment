﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguageEnvironment
{
    public class MoveToCommand: Command
    {
        private Point targetPosition;

        public MoveToCommand(Point target)
        {  
            this.targetPosition = target; 
        }

        public override void Execute(CanvasRenderer renderer)
        {
            renderer.MoveTo(targetPosition);
        }
        public Point TargetPosition => targetPosition;

    }
}
