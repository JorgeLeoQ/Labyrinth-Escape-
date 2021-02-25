using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labyrinth
{
    public class Key
    {
        private Vector2 keyPosA;
        private Point keyPosR;

        public Key(Vector2 keyPosA, Point keyPosR)
        {
            this.keyPosA = keyPosA;
            this.keyPosR = keyPosR;
        }

        public Vector2 KeyPosA
        {
            get { return this.keyPosA; }
        }
        public Point KeyPosR
        {
            get { return this.keyPosR; }
        }
    }
}
