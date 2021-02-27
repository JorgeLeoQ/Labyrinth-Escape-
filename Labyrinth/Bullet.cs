using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Labyrinth
{
    public enum Direction{ LEFT, RIGHT, UP, DOWN};
    public class Bullet

    {
        public Vector2 bulletPosA;
        private Vector2 startPos;
        public Point bulletPosR;
        public Point movePosR;
        private float bound;
        private Direction direction;
        private Rectangle hitBox;

        public Bullet(Point bulletPosR, Vector2 bulletPosA, Direction direction)
        {
            this.bulletPosA = bulletPosA;
            this.startPos = bulletPosA;
            this.bulletPosR = bulletPosR;
            this.movePosR = bulletPosR;
            this.direction = direction;
            this.bound = this.FindBound();
            this.hitBox = new Rectangle(bulletPosR.X, bulletPosR.Y, 20, 20);
        }

        public Rectangle HitBox
        {
            get { return this.hitBox; }
        }
        public float Bound
        {
            get { return this.bound; }
        }
        public Vector2 StartPos
        {
            get { return this.startPos; }
        }
        public Direction Direction
        {
            get { return this.direction; }
        }

        public float X
        {
            get { return bulletPosA.X; }
            set {
                bulletPosA.X = value; }
        }

        public float Y
        {
            get { return bulletPosA.Y; }
            set { bulletPosA.Y = value; }

        }

        //Private method that calculates the distance between the cannon and the opposite wall
        private float FindBound()
        {
            int i;
            Point tempP = bulletPosR;
            switch (this.direction)
            {
                case Direction.RIGHT:
                    for (i = 0; i < C.colsNb; i++)
                    {
                        if (C.lbrnt[tempP.X, tempP.Y] != '1')
                        {
                            tempP.Y--;
                        }
                        else
                        {
                            return ((float)i * C.multFactor);
                        }
                    }
                    break;
                case Direction.UP:
                    for (i = 0; i < C.rowsNb; i++)
                    {
                        if (C.lbrnt[tempP.X, tempP.Y] != '1')
                        {
                            tempP.X++;
                        }
                        else
                        {
                            return ((float)i * C.multFactor);
                        }
                    }
                    break;
                case Direction.DOWN:
                    for (i = 0; i < C.rowsNb; i++)
                    {
                        if (C.lbrnt[tempP.X, tempP.Y] != '1')
                        {
                            tempP.X++;
                        }
                        else
                        {
                            return ((float)i * C.multFactor + 40);
                        }
                    }
                    break;
                default:
                    for (i = 0; i < C.colsNb; i++)
                    {
                        if (C.lbrnt[tempP.X, tempP.Y] != '1')
                        {
                            tempP.Y++;
                        }
                        else
                        {
                            return ((float)i * C.multFactor);
                        }
                    }
                    break;
            }
            return -1;
        }
    }
}
