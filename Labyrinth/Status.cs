using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Threading;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth
{
    public static class Status
    {

        public static void DrawSatus( SpriteBatch _spriteBatch, GraphicsDevice GraphicsDevice, Character character)
        {
            switch (C.gameStatus)
            {
                case GameStatus.PLAY:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    DrawLabyrinth(_spriteBatch);

                    DrawBullets(_spriteBatch);

                    DrawKey(_spriteBatch);

                    DrawLife(_spriteBatch);

                    C.timeLabel.Draw();
                    C.levelLabel.Draw();
                    C.health.Draw();
                    C.keyLabel.Draw();
                    _spriteBatch.Draw(C.lifeImag, new Rectangle(30, 40, 60, 60), Color.White);
                    _spriteBatch.Draw(C.keyImag, new Rectangle(30, 100, 60, 60), Color.White);
                    C.backMenu2Button.Draw();
                    character.Draw(_spriteBatch);
                    break;
                case GameStatus.MENU:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(C.labImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(C.labImage.Width, C.labImage.Height)), Color.White);
                    C.startButton.Draw();
                    C.quitButton.Draw();
                    C.instructionButton.Draw();
                    C.creditButton.Draw();
                    break;
                case GameStatus.INSTRUCTION:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(C.instructionImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1500) / 2, 0), new Point(1500, 900)), new Rectangle(new Point(0, 0), new Point(C.instructionImage.Width, C.instructionImage.Height)), Color.White);
                    C.backMenuButton.Draw();
                    break;
                case GameStatus.CREDITS:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(C.creditsImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1500) / 2, 0), new Point(1500, 900)), new Rectangle(new Point(0, 0), new Point(C.creditsImage.Width, C.creditsImage.Height)), Color.White);
                    C.backMenuButton.Draw();
                    break;
                case GameStatus.LOSE:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(C.background,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(C.background.Width, C.background.Height)), Color.White);
                    _spriteBatch.Draw(C.finalScoreImag,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1150) / 2, 10), new Point(3000, 2000)), new Rectangle(new Point(0, 0), new Point(C.background.Width, C.background.Height)), Color.White);
                    _spriteBatch.Draw(C.gameOver,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 850) / 2, 65), new Point(800, 500)), new Rectangle(new Point(0, 0), new Point(C.background.Width, C.background.Height)), Color.White);
                    C.backMenu3Button.Draw();
                    C.retry.Draw();
                    break;
                case GameStatus.ENDGAME:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(C.background,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(C.background.Width, C.background.Height)), Color.White);
                    _spriteBatch.Draw(C.finalScoreImag,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1150) / 2, 10), new Point(3000, 2000)), new Rectangle(new Point(0, 0), new Point(C.background.Width, C.background.Height)), Color.White);
                    DrawFinalString(_spriteBatch);
                    C.backMenu3Button.Draw();
                    C.retry.Draw();
                    break;
            }
        }


        private static void DrawLabyrinth(SpriteBatch _spriteBatch)
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

        private static void DrawBullets(SpriteBatch _spriteBatch)
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

        private static void DrawLife(SpriteBatch _spriteBatch)
        {
            foreach (Life life in C.listLife)
            {
                _spriteBatch.Draw(C.lifeImag, life.HealthPosA, Color.White);
            }
        }

        private static void DrawKey(SpriteBatch _spriteBatch)
        {

            foreach (Key key in C.listKeys)
            {
                _spriteBatch.Draw(C.keyImag, key.KeyPosA, Color.White);
            }
        }

        private static void DrawFinalString(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(C.font, "CONGRATULATION", new Vector2(550, 100), Color.Black);
            _spriteBatch.DrawString(C.font, "You have finished all the labyrinths", new Vector2(400, 200), Color.Black);
            _spriteBatch.DrawString(C.font, "Your time: " + (int)C.finalTime, new Vector2(620, 300), Color.Black);
            _spriteBatch.DrawString(C.font, "Best time: " + C.bestTime, new Vector2(620, 400), Color.Black);
        }


    }
}
