using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Labyrinth
{

    public partial class Character
    {
        private const double delay =  0.10f;
        public Vector2 pos;

        Animation walkDown;
        Animation walkUp;
        Animation walkLeft;
        Animation walkRight;

        Animation standUp;
        Animation standDown;
        Animation standRight;
        Animation standLeft;

        public Animation currentAnimation;
        Animation lastAnimation;

        
        public Character(GraphicsDevice graphicsDevice)
        {
            createAnimation();
        }

        public void createAnimation()
        {
            C.charRect = new Rectangle(0, 0, C.charSheetText.Width/4, C.charSheetText.Height/8);
            int rectTopLeft = 0;
            int i;

            walkDown = new Animation();
            for (i=0; i < C.FRAMEXROW; i++)
            {
                walkDown.AddFrame(new Rectangle(i * C.charRect.Width, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);
            }
            standDown = new Animation();
            standDown.AddFrame(new Rectangle(0, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);

            rectTopLeft += C.charRect.Height;
            walkLeft = new Animation();
            for (i = 0; i < C.FRAMEXROW; i++)
            {
                walkLeft.AddFrame(new Rectangle(i * C.charRect.Width, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);
            }
            standLeft = new Animation();
            standLeft.AddFrame(new Rectangle(0, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);

            rectTopLeft += C.charRect.Height;
            walkRight = new Animation();
            for (i = 0; i < C.FRAMEXROW; i++)
            {
                walkRight.AddFrame(new Rectangle(i * C.charRect.Width, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);
            }
            standRight = new Animation();
            standRight.AddFrame(new Rectangle(0, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);

            rectTopLeft += C.charRect.Height;
            walkUp = new Animation();
            for (i = 0; i < C.FRAMEXROW; i++)
            {
                walkUp.AddFrame(new Rectangle(i * C.charRect.Width, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);
            }
            standUp = new Animation();
            standUp.AddFrame(new Rectangle(0, rectTopLeft, C.charRect.Width, C.charRect.Height), delay);

            lastAnimation = standDown;
            C.animationOn = false;
        }

        public void Update (GameTime gameTime)
        {
            if(C.animationOn)
            {
                currentAnimation.Update(gameTime);
            }
            else
            {
                if(Moving())
                {
                    C.animationOn = true;
                    currentAnimation.Update(gameTime);
                }

            }
        }
        public void Draw(SpriteBatch sb)
        {
            Vector2 topLeftOfFrame = new Vector2(0, 0);
            Rectangle sourceRectangle;

            if (C.animationOn)
            {
                if (currentAnimation == walkDown)
                {
                    topLeftOfFrame = new Vector2((float)C.guyPos.Y * C.multFactor + C.ORGLAB.X,
                                                    (float)C.guyPos.X * C.multFactor + C.ORGLAB.Y + currentAnimation.currFrameNb * C.step);
                }
                else if (currentAnimation == walkUp)
                {
                    topLeftOfFrame = new Vector2((float)C.guyPos.Y * C.multFactor + C.ORGLAB.X,
                                                       (float)C.guyPos.X * C.multFactor + C.ORGLAB.Y - currentAnimation.currFrameNb * C.step);
                }
                else if (currentAnimation == walkRight)
                {
                    topLeftOfFrame = new Vector2((float)C.guyPos.Y * C.multFactor + C.ORGLAB.X + currentAnimation.currFrameNb * C.step,
                                                   (float)C.guyPos.X * C.multFactor + C.ORGLAB.Y);
                }
                else if (currentAnimation == walkLeft)
                {
                    topLeftOfFrame = new Vector2((float)C.guyPos.Y * C.multFactor + C.ORGLAB.X - currentAnimation.currFrameNb * C.step,
                                                    (float)C.guyPos.X * C.multFactor + C.ORGLAB.Y);
                }
                sourceRectangle = currentAnimation.currentRectangle;
            }
            else
            {
                topLeftOfFrame = new Vector2((float)C.guyPos.Y * C.multFactor + C.ORGLAB.X,
                                                    (float)C.guyPos.X * C.multFactor + C.ORGLAB.Y);
                sourceRectangle = lastAnimation.frames[0].sourceRectangle;
            }
            sb.Draw(C.charSheetText, new Rectangle((int)topLeftOfFrame.X, (int)topLeftOfFrame.Y, (int)C.multFactor, (int)C.multFactor), sourceRectangle, Color.White);
        }
    }
}

