using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace Labyrinth
{
    public static class Colission
    {
        static private int pixelU = 0;
        static private int pixelL = 0;
        static private int pixelR = 0;
        static private int pixelD = 0;
            
        public static void ColissionBull()
        {
            //Gestione dei movimenti del proiettile
            foreach (Bullet bullet in C.listBulletR)
            {
                if (bullet.X > (bullet.StartPos.X - bullet.Bound))
                {
                    bullet.X -= 1.0f;
                    if (pixelR < C.multFactor)
                    {
                        pixelR++;
                    }
                    else
                    {
                        bullet.movePosR.Y -= 1;
                        pixelR = 0;
                        if ((bullet.movePosR.X) == C.guyPos.X && bullet.movePosR.Y == C.guyPos.Y)
                        {
                            C.explotionEffect.Play();
                            C.life--;
                            C.isExplotion = true;
                            if(C.life == 0)
                            {
                                C.gameStatus = GameStatus.LOSE;
                            }
                            bullet.X = bullet.StartPos.X;
                            bullet.movePosR.Y = bullet.bulletPosR.Y;
                        }
                    }
                }
                else
                {
                    bullet.X = bullet.StartPos.X;
                    bullet.movePosR.Y = bullet.bulletPosR.Y;
                    pixelR = 0;
                }
            }
            foreach (Bullet bullet in C.listBulletL)
            {
                if (bullet.X < (bullet.StartPos.X + bullet.Bound))
                {
                    bullet.X += 1.0f;
                    if (pixelL < C.multFactor)
                    {
                        pixelL++;
                    }
                    else
                    {
                        bullet.movePosR.Y += 1;
                        pixelL = 0;
                        if ((bullet.movePosR.X) == C.guyPos.X && bullet.movePosR.Y == C.guyPos.Y)
                        {
                            C.explotionEffect.Play();
                            C.life--;
                            C.isExplotion = true;
                            if (C.life == 0)
                            {
                                C.gameStatus = GameStatus.LOSE;
                                bullet.X = bullet.StartPos.X;
                                bullet.movePosR.Y = bullet.bulletPosR.Y;
                            }
                        }
                    }
                }
                else
                {
                    bullet.X = bullet.StartPos.X;
                    bullet.movePosR.Y = bullet.bulletPosR.Y;
                    pixelL = 0;
                }
            }
            foreach (Bullet bullet in C.listBulletU)
            {
                if (bullet.Y < (bullet.StartPos.Y + bullet.Bound))
                {
                    bullet.Y += 1.0f;
                    if (pixelU < C.multFactor)
                    {
                        pixelU++;
                    }
                    else
                    {
                        bullet.movePosR.X += 1;
                        pixelU = 0;
                        if ((bullet.movePosR.X) == C.guyPos.X && bullet.movePosR.Y == C.guyPos.Y)
                        {
                            C.explotionEffect.Play();
                            C.life--;
                            C.isExplotion = true;
                            if (C.life == 0)
                            {
                                C.gameStatus = GameStatus.LOSE;
                            }
                            bullet.Y = bullet.StartPos.Y;
                            bullet.movePosR.X = bullet.bulletPosR.X;
                        }
                    }
                }
                else
                {
                    bullet.Y = bullet.StartPos.Y;
                    bullet.movePosR.X = bullet.bulletPosR.X;
                    pixelU = 0;
                }
            }
            foreach (Bullet bullet in C.listBulletD)
            {
                if (bullet.Y > (bullet.StartPos.Y - bullet.Bound))
                {
                    bullet.Y -= 1.0f;
                    if (pixelD < C.multFactor)
                    {
                        pixelD++;
                    }
                    else
                    {
                        bullet.movePosR.X -= 1;
                        pixelD = 0;
                        if ((bullet.movePosR.X) == C.guyPos.X && bullet.movePosR.Y == C.guyPos.Y)
                        {
                            C.explotionEffect.Play();
                            C.life--;
                            C.isExplotion = true;
                            if (C.life == 0)
                            {
                                C.gameStatus = GameStatus.LOSE;
                            }
                            bullet.Y = bullet.StartPos.Y;
                            bullet.movePosR.X = bullet.bulletPosR.X;
                        }
                    }
                }
                else
                {
                    bullet.Y = bullet.StartPos.Y;
                    bullet.movePosR.X = bullet.bulletPosR.X;
                    pixelD = 0;
                }
            }
        }

        public static void ColissionLive()
        {
            for(int i = 0; i < C.listLife.Count; i++)
            {
                if(C.guyPos == C.listLife[i].HealthPosR)
                {
                    C.listLife.RemoveAt(i);
                    C.life++;
                    C.lifeEffect.Play();
                }
            }
        }

        public static void CollisionKey()
        {
            for(int i = 0; i < C.listKeys.Count; i++)
            {
                if(C.guyPos == C.listKeys[i].KeyPosR)
                {
                    C.listKeys.RemoveAt(i);
                    C.keys += 1;
                    C.keyEffect.Play();
                }
            }
        }
    }
}
