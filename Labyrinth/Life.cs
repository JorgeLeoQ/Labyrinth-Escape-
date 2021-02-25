using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labyrinth
{
    public class Life
    {
        private Vector2 lifePosA;
        private Point lifePosR;
        public Life(Vector2 lifePosA, Point lifePosR)
        {
            this.lifePosA = lifePosA;
            this.lifePosR = lifePosR;
        }

        public Vector2 HealthPosA
        {
            get { return this.lifePosA; }
        }

        public Point HealthPosR
        {
            get { return this.lifePosR; }
        }
    }
}
