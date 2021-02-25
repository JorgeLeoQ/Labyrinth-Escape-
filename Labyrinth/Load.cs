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
        //Load information about Bullets
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

        //Load information about life
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

        //Load information about maze and position guy
        public static void LoadMaze(string[] lines)
        {
            char[] line = lines[0].ToCharArray();
            C.colsNb = line.GetLength(0);
            C.rowsNb = lines.GetLength(0);

            C.ORGLAB = new Vector2((C.DISPLAYDIM.X - C.colsNb * C.multFactor) / 2,
                                 (C.DISPLAYDIM.Y - C.rowsNb * C.multFactor) / 2);

            C.lbrnt = new int[(int)C.rowsNb, (int)C.colsNb];

            for (int i = 0; i < C.rowsNb; i++)
            {
                line = lines[i].ToCharArray();
                for (int j = 0; j < C.colsNb; j++)
                {
                    C.lbrnt[i, j] = line[j];
                    if (C.lbrnt[i, j] == '0')
                    {
                        if (j == 0)
                        {
                            C.startGuyPos = C.guyPos = new Point(i, 0);
                        }
                    }
                    else if (C.lbrnt[i, j] == 'P')
                    {
                        if (j == C.colsNb - 1)
                        {
                            C.endGuyPos = new Point(i, C.colsNb - 1);
                        }
                    }
                }
                C.labText = new Texture2D[((int)(C.rowsNb) * (int)C.multFactor),
                                          ((int)C.colsNb * (int)C.multFactor)];
                if (C.gameStatus != GameStatus.MENU)
                    C.gameStatus = GameStatus.PLAY;
            }

        }
    }
}
