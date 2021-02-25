using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Labyrinth
{
    public partial class Game1
    {
        private string fileName= "C://Users//Jorge//Desktop//Labyrinth//Labyrinth-Escape-//Labyrinth//Content//labirinto" + C.level.ToString() + ".txt";
        public void startGame()
        {

            C.charSheetText = Content.Load<Texture2D>("mainChar");
            C.wallTile = Content.Load<Texture2D>("wallTile");
            C.grassTile = Content.Load<Texture2D>("grassTile");
            C.cannonL = Content.Load<Texture2D>("CannoneL");
            C.cannonR = Content.Load<Texture2D>("CannoneR");
            C.cannonU = Content.Load<Texture2D>("CannoneU");
            C.cannonD = Content.Load<Texture2D>("CannoneD");
            C.bulletImag = Content.Load<Texture2D>("Bullet");
            C.padlockImag = Content.Load<Texture2D>("Padlock");
            C.lifeImag = Content.Load<Texture2D>("HeartL");
            C.keyImag = Content.Load<Texture2D>("Key");

            string[] lines = File.ReadAllLines(fileName);

            C.listBulletR.Clear();
            C.listBulletD.Clear();
            C.listBulletL.Clear();
            C.listBulletU.Clear();
            C.listKeys.Clear();
            C.listLife.Clear();

            char[] line = lines[0].ToCharArray();
            C.colsNb = line.GetLength(0);
            C.rowsNb = lines.GetLength(0);

            C.ORGLAB = new Vector2((C.DISPLAYDIM.X - C.colsNb * C.multFactor)/2 ,
                                 (C.DISPLAYDIM.Y - C.rowsNb * C.multFactor)/2);

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
                    else if(C.lbrnt[i, j] == 'P')
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

            Load.LoadBullets();
            Load.LoadLives();
        }
    }
}

