using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth
{
    public class Label
    {
        public Vector2 position;
        public Point dimension;
        public SpriteBatch sb;
        Color labelColor;
        public string labelString;
        Texture2D rect;
        GraphicsDevice graphicsDev;
        SpriteFont labelFont;
        Vector2 stringPos;

        public Label(Vector2 pos, Point dim, SpriteBatch sBatch, Color bC, string Str, GraphicsDevice gD, SpriteFont bF)
        {
            position = pos;
            dimension = dim;
            sb = sBatch;
            labelString = Str;
            graphicsDev = gD;
            labelFont = bF;
            Vector2 fontSize = labelFont.MeasureString(labelString);
            rect = new Texture2D(gD, dimension.X, dimension.Y);
            Color[] data = new Color[dimension.X * dimension.Y];
            for (int i = 0; i < data.Length; ++i) data[i] = labelColor;
            rect.SetData(data);
            stringPos = new Vector2((rect.Width - fontSize.X) / 2 + position.X, (rect.Height - fontSize.Y) / 2 + position.Y);
        }


        public void Draw()
        {
            sb.Draw(rect, position, Color.White);
            sb.DrawString(labelFont, labelString, stringPos, Color.White);
        }
    }
}
