using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using System.IO;

namespace Labyrinth
{
    public enum GameStatus { MENU, PLAY, INSTRUCTION, CREDITS, LOSE, ENDLEVEL, ENDGAME };
    public partial class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Character character;
        Texture2D labImage;
        Texture2D finalScoreImag;
        Texture2D gameOver;
        Texture2D background;
        Texture2D creditsImage;
        Texture2D instructionImage;


        Label timeLabel;
        Label health;
        Label keyLabel;
        Label levelLabel;
        Label endLabel;
       
        Button startButton;
        Button instructionButton;
        Button quitButton;
        Button creditButton;
        Button backMenuButton;
        Button backMenu2Button;
        Button backMenu3Button;
        Button retry;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        
        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = (int)C.DISPLAYDIM.X;  // set this value to the desired width of your window
            _graphics.PreferredBackBufferHeight = (int)C.DISPLAYDIM.Y;   // set this value to the desired height of your window
            _graphics.ApplyChanges();

            C.gameStatus = GameStatus.MENU;

            C.animationOn = false;


            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            Save.LoadBestTime();
            loadLevel();
            character = new Character(this.GraphicsDevice);
            
            LoadLabel();
            LoadButton();
            LoadTexture();
            LoadSoundEffect();

        }

        void MediaPlayer_MediaStateChanged(object sender, System.EventArgs s)
        {
            MediaPlayer.Volume -= 0.1f;
            MediaPlayer.Play(C.song);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if (C.gameStatus == GameStatus.ENDLEVEL)
            {
                loadLevel();

                base.Update(gameTime);
                return;
            }

            if (C.gameStatus == GameStatus.MENU)
            {
                if (startButton.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    loadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
                if (quitButton.Click())
                {
                    Exit();
                }
                if (instructionButton.Click())
                {
                    C.gameStatus = GameStatus.INSTRUCTION;
                }
                if (creditButton.Click())
                {
                    C.gameStatus = GameStatus.CREDITS;
                }
            }

            if (C.gameStatus == GameStatus.CREDITS)
            {
                if (backMenuButton.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                return;
            }

            if (C.gameStatus == GameStatus.PLAY)
            {
                if (backMenu2Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                    C.level = 0;
                }
                //Gestisce la collissione con le palottole.
                Colission.ColissionBull();
                Colission.ColissionLive();
                Colission.CollisionKey();
            }

            if (C.gameStatus == GameStatus.INSTRUCTION)
            {
                if (backMenuButton.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                return;
            }
            else
            {
                C.timeElapsed += gameTime.ElapsedGameTime.TotalSeconds;
                timeLabel.labelString = "Time: " + Math.Truncate(C.timeElapsed);
                levelLabel.labelString = "Level: " + C.level.ToString();
                health.labelString = C.life.ToString();
                keyLabel.labelString = C.keys.ToString();
                character.Update(gameTime);
            }

            if(C.gameStatus == GameStatus.ENDGAME)
            {
                if (backMenu3Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                if (retry.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    C.level = 0;
                    loadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
            }

            if(C.gameStatus == GameStatus.LOSE)
            {
                if (backMenu3Button.Click())
                {
                    C.gameStatus = GameStatus.MENU;
                }
                if (retry.Click())
                {
                    C.gameStatus = GameStatus.PLAY;
                    C.level = 0;
                    loadLevel();
                    C.keys = 0;
                    C.life = 3;
                    C.timeElapsed = 0;
                }
            }
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            switch (C.gameStatus)
            {
                case GameStatus.PLAY:
                    GraphicsDevice.Clear(Color.CornflowerBlue);

                    DrawPlayStatus.DrawLabyrinth(_spriteBatch);

                    DrawPlayStatus.DrawBullets(_spriteBatch);

                    DrawPlayStatus.DrawKey(_spriteBatch);

                    DrawPlayStatus.DrawLife(_spriteBatch);

                    timeLabel.Draw();
                    levelLabel.Draw();
                    health.Draw();
                    _spriteBatch.Draw(C.lifeImag, new Rectangle(30,40, 60, 60), Color.White);
                    _spriteBatch.Draw(C.keyImag, new Rectangle(30, 100, 60, 60), Color.White);
                    keyLabel.Draw();
                    backMenu2Button.Draw();
                    character.Draw(_spriteBatch);
                    break;
                case GameStatus.MENU:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(labImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(labImage.Width, labImage.Height)), Color.White);
                    startButton.Draw();
                    quitButton.Draw();
                    instructionButton.Draw();
                    creditButton.Draw();
                    break;
                case GameStatus.INSTRUCTION:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(instructionImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1500) / 2, 0), new Point(1500, 900)), new Rectangle(new Point(0, 0), new Point(instructionImage.Width, instructionImage.Height)), Color.White);
                    backMenuButton.Draw();
                    break;
                case GameStatus.CREDITS:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(creditsImage,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1500) / 2, 0), new Point(1500, 900)), new Rectangle(new Point(0, 0), new Point(creditsImage.Width, creditsImage.Height)), Color.White);
                    backMenuButton.Draw();
                    break;
                case GameStatus.LOSE:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(background,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(background.Width, background.Height)), Color.White);
                    _spriteBatch.Draw(finalScoreImag,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1150) / 2, 10), new Point(3000, 2000)), new Rectangle(new Point(0, 0), new Point(background.Width, background.Height)), Color.White);
                    _spriteBatch.Draw(gameOver,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 850) / 2, 65), new Point(800, 500)), new Rectangle(new Point(0, 0), new Point(background.Width, background.Height)), Color.White);
                    backMenu3Button.Draw();
                    retry.Draw();
                    break;
                case GameStatus.ENDGAME:
                    GraphicsDevice.Clear(Color.AntiqueWhite);
                    _spriteBatch.Draw(background,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1600) / 2, 0), new Point(1600, 900)), new Rectangle(new Point(0, 0), new Point(background.Width, background.Height)), Color.White);
                    _spriteBatch.Draw(finalScoreImag,
                        new Rectangle(new Point(((int)C.DISPLAYDIM.X - 1150) / 2, 10), new Point(3000, 2000)), new Rectangle(new Point(0, 0), new Point(background.Width, background.Height)), Color.White);
                    DrawText.DrawFinalString(_spriteBatch);
                    backMenu3Button.Draw();
                    retry.Draw();
                    break;
            }   
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        public void LoadButton()
        {
            C.font = Content.Load<SpriteFont>("Font");
            SpriteFont f = Content.Load<SpriteFont>("ButtonFont");
            startButton = new Button(new Vector2(100, 250), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "START", this.GraphicsDevice, f);
            instructionButton = new Button(new Vector2(100, 400), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "INSTRUCTION", this.GraphicsDevice, f);
            quitButton = new Button(new Vector2(100, 550), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "QUIT", this.GraphicsDevice, f);
            creditButton = new Button(new Vector2(100, 700), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "CREDITS", this.GraphicsDevice, f);
            backMenu2Button = new Button(new Vector2(1390, 600), new Point(200, 100), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, f);
            backMenu3Button = new Button(new Vector2(900, 500), new Point(250, 100), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, f);
            retry = new Button(new Vector2(400, 500), new Point(250, 100), _spriteBatch, Color.Green, Color.Aqua, "RETRY", this.GraphicsDevice, f);
            backMenuButton = new Button(new Vector2(650, 700), new Point(300, 100), _spriteBatch, Color.Green, Color.Aqua, "BACK\nMENU", this.GraphicsDevice, f);
        }

        public void LoadTexture()
        {
            labImage = Content.Load<Texture2D>("labiri");
            finalScoreImag = Content.Load<Texture2D>("Parchment");
            gameOver = Content.Load<Texture2D>("GameOver");
            background = Content.Load<Texture2D>("background");
            creditsImage = Content.Load<Texture2D>("Credits");
            instructionImage = Content.Load<Texture2D>("Instruction");
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

        public void LoadLabel()
        {
            levelLabel = new Label(new Vector2(1360, 20), new Point(200, 100), _spriteBatch, Color.Transparent, "Level: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            timeLabel = new Label(new Vector2(1360, 80), new Point(200, 100), _spriteBatch, Color.Transparent, "Time: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            health = new Label(new Vector2(40, 20), new Point(200, 100), _spriteBatch, Color.Transparent, "Life: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            keyLabel = new Label(new Vector2(40, 80), new Point(200, 100), _spriteBatch, Color.Transparent, "Life: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));

            endLabel = new Label(new Vector2(400, 500), new Point(200, 100), _spriteBatch, Color.Transparent, "End Game Your Score is: ", GraphicsDevice, Content.Load<SpriteFont>("ButtonFont"));
        }
    }
}
