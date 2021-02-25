using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth
{
    //Class that allows the addition of buttons and manage the click on them
    public class Button
    {
        public Vector2 position;
        public Point dimension;
        public SpriteBatch sb;
        Color buttonColor;
        string buttonString;
        Texture2D rect, rectMouseOver, curreMouseText;
        GraphicsDevice graphicsDev;
        SpriteFont buttonFont;
        Vector2 stringPos;
        Color mouseOver;
        MouseState mouseState;
        MouseState mouseOldState;
        Color currentButtonColor;
        Rectangle buttonRect;


        public Button(Vector2 pos, Point dim, SpriteBatch sBatch, Color bC, Color mO, string Str, GraphicsDevice gD, SpriteFont bF)
        {
            position = pos;
            dimension = dim;
            sb = sBatch;
            buttonColor = bC;
            buttonString = Str;
            graphicsDev = gD;
            buttonFont = bF;
            Vector2 fontSize = buttonFont.MeasureString(buttonString);
            mouseOver = mO;
            

            rect = new Texture2D(gD, dimension.X, dimension.Y);
            rectMouseOver = new Texture2D(gD, dimension.X, dimension.Y);

            Color[] data = new Color[dimension.X * dimension.Y];
            for (int i = 0; i < data.Length; ++i) data[i] = buttonColor;
            rect.SetData(data);
            for (int i = 0; i < data.Length; ++i) data[i] = mouseOver;
            rectMouseOver.SetData(data);
            stringPos = new Vector2((rect.Width - fontSize.X) / 2 + position.X, (rect.Height - fontSize.Y) / 2 + position.Y);
            mouseState = Mouse.GetState();
            currentButtonColor = buttonColor;
            buttonRect = new Rectangle(new Point((int)position.X, (int)position.Y), dimension);
        }

        public bool Click()
        {
            mouseState = Mouse.GetState();
            if (buttonRect.Contains(new Point(mouseState.X, mouseState.Y)))
            {
                curreMouseText = rectMouseOver;
                if(mouseState.LeftButton == ButtonState.Pressed)
                {
                    C.buttonEffect.Play();
                    return true;
                }

            } else
            {
                curreMouseText = rect;
                return false;
            }
            return false;
        }

        public void Draw()
        {
            sb.Draw(curreMouseText, position, Color.White);
            sb.DrawString(buttonFont, buttonString, stringPos, Color.White);
        }
    }
}
