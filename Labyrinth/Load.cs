using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labyrinth
{
    public static class Load
    {
        public static void LoadBullets()
        {
            Vector2 pos = C.ORGLAB;
            for (int i = 0; i < C.rowsNb; i++, pos.Y += C.multFactor, pos.X = C.ORGLAB.X)
            {
                for (int j = 0; j < C.colsNb; j++, pos.X += C.multFactor)
                {
                    switch (C.lbrnt[i, j])
                    {
                        case 'U':
                            C.listBulletU.Add(new Bullet(new Point(i, j), pos, Direction.UP));
                            break;
                        case 'D':
                            C.listBulletD.Add(new Bullet(new Point(i, j), pos, Direction.DOWN));
                            break;
                        case 'L':
                            C.listBulletL.Add(new Bullet(new Point(i, j), pos, Direction.LEFT));
                            break;
                        case 'R':
                            C.listBulletR.Add(new Bullet(new Point(i, j), pos, Direction.RIGHT));
                            break;
                        case 'C':
                            C.listKeys.Add(new Key(pos, new Point(i, j)));
                            break;
                    }

                }
            }
        }
        public static void LoadLives()
        {
            int count = 0;
            int lifes = (1 + C.rand.Next(0, 5));
            int colsRand;
            int rowRand;

            while (count != lifes)
            {
                colsRand = C.rand.Next(0, C.colsNb);
                rowRand = C.rand.Next(0, C.rowsNb);
                Vector2 pos = C.ORGLAB;
                for (int i = 0; i < C.rowsNb; i++, pos.Y += C.multFactor, pos.X = C.ORGLAB.X)
                {
                    for (int j = 0; j < C.colsNb; j++, pos.X += C.multFactor)
                    {
                        if (i == rowRand && j == colsRand)
                        {
                            if (C.lbrnt[i, j] == '0')
                            {
                                C.listLife.Add(new Life(pos, new Point(i, j)));
                                count++;
                            }
                        }
                    }
                }
            }
        }
    }
}
