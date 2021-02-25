using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
    public class Cannon
    {
        public Texture2D cannonImag;
        public Point cannonPos;

        public Cannon(Texture2D cannonImag, Point cannonPos)
        {
            this.cannonImag = cannonImag;
            this.cannonPos = cannonPos;
        }

        public int X
        {
            get { return cannonPos.X; }
            set { this.cannonPos.X = value; }
        }

        public int Y
        {
            get { return this.cannonPos.Y; }
            set { this.cannonPos.Y = value; }
        }
    }
}
