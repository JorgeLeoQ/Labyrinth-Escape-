using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.IO;

namespace Labyrinth
{
    public partial class Character
    {
        KeyboardState kbState;
        bool kbPressed = false;
        public Rectangle rectPos;
        public bool Moving()
        {
            kbState = Keyboard.GetState();
            var keysPressed = kbState.GetPressedKeys();
            if (keysPressed.Length == 0)
            {
                kbPressed = false;
                return false;
            }

            if (kbPressed)
            {
                return false;
            }

            kbPressed = true;
            switch(keysPressed[0])
            {
                case Keys.S:
                    if(C.lbrnt[(int)C.guyPos.X+1, (int)C.guyPos.Y] == '0' || C.lbrnt[(int)C.guyPos.X + 1, (int)C.guyPos.Y] == 'C')
                    {
                        currentAnimation = walkDown;
                        lastAnimation = standDown;
                        C.movingDir = new Point(1, 0);
                        return true;
                    }
                    break;
                case Keys.W:
                    if (C.lbrnt[(int)C.guyPos.X - 1, (int)C.guyPos.Y] == '0' || C.lbrnt[(int)C.guyPos.X - 1, (int)C.guyPos.Y] == 'C')
                    {
                        currentAnimation = walkUp;
                        lastAnimation = standUp;
                        C.movingDir = new Point(-1, 0);
                        return true;
                    }
                    break;
                case Keys.A:
                    if ((C.guyPos != C.startGuyPos) && 
                        (C.lbrnt[(int)C.guyPos.X, (int)C.guyPos.Y - 1] == '0' || C.lbrnt[(int)C.guyPos.X, (int)C.guyPos.Y - 1] == 'C'))
                    {
                        currentAnimation = walkLeft;
                        lastAnimation = standLeft;
                        C.movingDir = new Point(0, -1);
                        return true;
                    }
                    
                    break;
                case Keys.D:

                    if (C.guyPos == C.endGuyPos)
                    {
                        if(C.keys > 0)
                        {
                            C.level++;
                            if (C.level == C.MAXLEVEL)
                            {
                                C.finalTime = C.timeElapsed;
                                if(C.finalTime < C.bestTime)
                                {
                                    C.bestTime = (int)C.finalTime;

                                    Save.SaveBestTime(C.bestTime.ToString());
                                }
                                C.gameStatus = GameStatus.ENDGAME;
                            }
                            else
                            {
                                C.keys--;
                                C.openLookEffect.Play();
                                C.gameStatus = GameStatus.ENDLEVEL;
                            }
                        }
                        return false;
                    }
                    if ((C.guyPos != C.endGuyPos) &&  
                        (C.lbrnt[(int)C.guyPos.X, (int)C.guyPos.Y + 1] == '0' || C.lbrnt[(int)C.guyPos.X, (int)C.guyPos.Y + 1] == 'C' || C.lbrnt[(int)C.guyPos.X, (int)C.guyPos.Y + 1] == 'P'))
                    {
                        currentAnimation = walkRight;
                        lastAnimation = standRight;
                        C.movingDir = new Point(0, 1);
                        rectPos = new Rectangle(C.guyPos.X, C.guyPos.Y, 40, 40);
                        return true;
                    }
                    break;
            }
            return false;
        }
    }
}
