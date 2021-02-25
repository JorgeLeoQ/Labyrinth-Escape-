using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace Labyrinth
{
    public class Animation
    {
        public List<AnimationFrame> frames = new List<AnimationFrame>();
        public Rectangle currentRectangle;
        public int currFrameNb;
        double secondsIntoAnimation;

        double duration
        {
            get
            {
                double totalSeconds = 0;
                foreach(var frame in frames)
                {
                    totalSeconds += frame.duration;
                }
                return totalSeconds;
            }
        }

        public void AddFrame(Rectangle r, double d)
        {
            AnimationFrame newFrame = new AnimationFrame();
            newFrame.sourceRectangle = r;
            newFrame.duration = d;
            frames.Add(newFrame);
        }

        public void Update(GameTime gameTime)
        {
            
            if (!C.animationOn)
                return;
            secondsIntoAnimation += gameTime.ElapsedGameTime.TotalSeconds;
            AnimationFrame currentFrame = null;

            double accumulatedTime = 0;
            currFrameNb = 0;
            int i;
            for(i=0; i < frames.Count; i++)
            {
                if (accumulatedTime + frames[i].duration >= secondsIntoAnimation)
                {
                    currentFrame = frames[i];
                    break;
                } else
                {
                    currFrameNb++;
                    accumulatedTime += frames[i].duration;
                }
            }
            if(i >= frames.Count)
            {
                C.animationOn = false;
                secondsIntoAnimation = 0;
                C.guyPos += C.movingDir;
            }
            if(currentFrame == null)
            {
                currentFrame = frames.LastOrDefault();
                C.animationOn = false;
            } else
            {
                currentRectangle = currentFrame.sourceRectangle;
                if (currentRectangle == Rectangle.Empty)
                {
                    currentRectangle = C.charRect;
                }
            }
        }
    }
}
