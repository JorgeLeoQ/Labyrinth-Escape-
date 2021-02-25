using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace Labyrinth
{
    public static class DrawText
    {
        //Handles the strings on the screen
        public static void DrawFinalString(SpriteBatch _spriteBatch)
        {
            _spriteBatch.DrawString(C.font, "CONGRATULATION", new Vector2(550, 100), Color.Black);
            _spriteBatch.DrawString(C.font, "You have finished all the labyrinths", new Vector2(400, 200), Color.Black);
            _spriteBatch.DrawString(C.font, "Your time: " + (int)C.finalTime, new Vector2(620, 300), Color.Black);
            _spriteBatch.DrawString(C.font, "Best time: " + C.bestTime, new Vector2(620, 400), Color.Black);
        }


    }
}
