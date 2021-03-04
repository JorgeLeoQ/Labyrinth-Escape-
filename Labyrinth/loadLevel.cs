using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Labyrinth
{
    public partial class Game1
    { 
        //Upload information on the different levels
        public void LoadLevel()
        {
            string fileName = "C://Users//Jorge//Desktop//Labyrinth//Labyrinth-Escape-//Labyrinth//Content//labirinto" + C.level.ToString() + ".txt";
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

            LoadMaze(lines);
            LoadBullets();
            LoadLives();
        }

        public void UpdateState(GameTime gameTime)
        {

            if (C.gameStatus == GameStatus.ENDLEVEL)
            {
                LoadLevel();

                base.Update(gameTime);
                return;
            }

            if (C.gameStatus == GameStatus.MENU)
            {
                if (C.startButton.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    LoadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
                if (C.quitButton.Click())
                {
                    Exit();
                }
                if (C.instructionButton.Click())
                {
                    C.gameStatus = GameStatus.INSTRUCTION;
                }
                if (C.creditButton.Click())
                {
                    C.gameStatus = GameStatus.CREDITS;
                }
            }

            if (C.gameStatus == GameStatus.CREDITS)
            {
                if (C.backMenuButton.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                return;
            }

            if (C.gameStatus == GameStatus.PLAY)
            {
                if (C.backMenu2Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                    C.level = 0;
                }
                //Gestisce la collissione con le palottole.
                Collision.CollisionBullet();
                Collision.CollisionLife();
                Collision.CollisionKey();
            }

            if (C.gameStatus == GameStatus.INSTRUCTION)
            {
                if (C.backMenuButton.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                return;
            }
            else
            {
                C.timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
                C.timeLabel.labelString = "Time: " + Math.Truncate(C.timeElapsed);
                C.levelLabel.labelString = "Level: " + C.level.ToString();
                C.health.labelString = C.life.ToString();
                C.keyLabel.labelString = C.keys.ToString();
                character.Update(gameTime);
            }

            if (C.gameStatus == GameStatus.ENDGAME)
            {
                if (C.backMenu3Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                if (C.retry.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    C.level = 0;
                    LoadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
            }

            if (C.gameStatus == GameStatus.LOSE)
            {
                if (C.backMenu3Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                if (C.retry.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    C.level = 0;
                    LoadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
            }
        }

        //Load information about Bullets
        private void LoadBullets()
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
        private void LoadLives()
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
        private void LoadMaze(string[] lines)
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

        public void LoadButton()
        {
            C.font = Content.Load<SpriteFont>("Font");
            SpriteFont f = Content.Load<SpriteFont>("ButtonFont");
            SpriteFont liteF = Content.Load<SpriteFont>("back");
            C.startButton = new Button(new Vector2(600, 250), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "START", this.GraphicsDevice, f);
            C.instructionButton = new Button(new Vector2(600, 400), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "INSTRUCTION", this.GraphicsDevice, f);
            C.quitButton = new Button(new Vector2(600, 700), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "QUIT", this.GraphicsDevice, f);
            C.creditButton = new Button(new Vector2(600, 550), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "CREDITS", this.GraphicsDevice, f);
            C.backMenu2Button = new Button(new Vector2(40, 600), new Point(130, 70), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, liteF);
            C.backMenu3Button = new Button(new Vector2(900, 500), new Point(250, 100), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, f);
            C.retry = new Button(new Vector2(400, 500), new Point(250, 100), _spriteBatch, Color.Green, Color.Aqua, "RETRY", this.GraphicsDevice, f);
            C.backMenuButton = new Button(new Vector2(1100, 700), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, f);
        }

        public void LoadTexture()
        {
            C.labImage = Content.Load<Texture2D>("labiri");
            C.finalScoreImag = Content.Load<Texture2D>("Parchment");
            C.gameOver = Content.Load<Texture2D>("GameOver");
            C.background = Content.Load<Texture2D>("background");
            C.creditsImage = Content.Load<Texture2D>("Credits");
            C.instructionImage = Content.Load<Texture2D>("Instruction");
        }

        public void LoadSoundEffect()
        {
            C.explotionEffect = Content.Load<SoundEffect>("explosion");
            C.keyEffect = Content.Load<SoundEffect>("KeyEffect");
            C.buttonEffect = Content.Load<SoundEffect>("lifeEffect");
            C.openLookEffect = Content.Load<SoundEffect>("Locked");
            C.lifeEffect = Content.Load<SoundEffect>("Tasto2Effect");

            C.song = Content.Load<Song>("African_Musicic");
            MediaPlayer.Play(C.song);
            MediaPlayer.MediaStateChanged += MediaPlayer_MediaStateChanged;
            MediaPlayer.IsRepeating = true;
        }

        private void LoadLabel()
        {
            C.levelLabel = new Label(new Vector2(1420, 20), new Point(120, 100), _spriteBatch, Color.Transparent, "Level: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            C.timeLabel = new Label(new Vector2(1420, 80), new Point(120, 100), _spriteBatch, Color.Transparent, "Time: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            C.health = new Label(new Vector2(40, 20), new Point(200, 100), _spriteBatch, Color.Transparent, "Life: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            C.keyLabel = new Label(new Vector2(40, 80), new Point(200, 100), _spriteBatch, Color.Transparent, "Life: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

        }
    }
}

