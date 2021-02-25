using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth
{
    public static class DrawC
    {
        public static void DrawLabyrinth(SpriteBatch _spriteBatch)
        {
            Vector2 pos = C.ORGLAB;
            for (int i = 0; i < C.rowsNb; i++, pos.Y += C.multFactor, pos.X = C.ORGLAB.X)
            {
                for (int j = 0; j < C.colsNb; j++, pos.X += C.multFactor)
                {
                    switch (C.lbrnt[i, j])
                    {
                        case '1':
                            _spriteBatch.Draw(C.wallTile, pos, Color.White);
                            break;
                        case '0':
                            _spriteBatch.Draw(C.grassTile, pos, Color.White);
                            break;
                        case 'C':
                            _spriteBatch.Draw(C.grassTile, pos, Color.White);
                            break;
                        case 'U':
                            _spriteBatch.Draw(C.cannonU, pos, Color.White);
                            break;
                        case 'D':
                            _spriteBatch.Draw(C.cannonD, pos, Color.White);
                            break;
                        case 'L':
                            _spriteBatch.Draw(C.cannonL, pos, Color.White);
                            break;
                        case 'R':
                            _spriteBatch.Draw(C.cannonR, pos, Color.White);
                            break;
                        case 'P':
                            _spriteBatch.Draw(C.wallTile, pos, Color.White);
                            _spriteBatch.Draw(C.padlockImag, pos, Color.White);
                            break;
                    }
                }
            }
        }

        public static void DrawBullets(SpriteBatch _spriteBatch)
        {

            foreach (Bullet bullut in C.listBulletR)
            {
                _spriteBatch.Draw(C.bulletImag, bullut.bulletPosA, Color.White);
                if (C.isExplotion)
                {
                    C.guyPos = C.startGuyPos;
                    C.isExplotion = false;
                    Thread.Sleep(1000);
                }
            }
            foreach (Bullet bullut in C.listBulletL)
            {
                _spriteBatch.Draw(C.bulletImag, bullut.bulletPosA, Color.White);
                if (C.isExplotion)
                {
                    C.guyPos = C.startGuyPos;
                    C.isExplotion = false;
                    Thread.Sleep(1000);
                }
            }
            foreach (Bullet bullut in C.listBulletU)
            {
                _spriteBatch.Draw(C.bulletImag, bullut.bulletPosA, Color.White);
                if (C.isExplotion)
                {
                    C.guyPos = C.startGuyPos;
                    C.isExplotion = false;
                    Thread.Sleep(1000);
                }
            }
            foreach (Bullet bullut in C.listBulletD)
            {
                _spriteBatch.Draw(C.bulletImag, bullut.bulletPosA, Color.White);
                if (C.isExplotion)
                {
                    C.guyPos = C.startGuyPos;
                    C.isExplotion = false;
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
