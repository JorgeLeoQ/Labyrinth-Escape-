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
        
        //Upload information on the different levels
        public void loadLevel()
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

            Load.LoadMaze(lines);
            Load.LoadBullets();
            Load.LoadLives();
        }
    }
}

