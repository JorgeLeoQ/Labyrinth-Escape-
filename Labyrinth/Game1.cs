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
            LoadLevel();
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

            UpdateState(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            _spriteBatch.Begin();

            Status.DrawSatus(_spriteBatch, GraphicsDevice, character);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
