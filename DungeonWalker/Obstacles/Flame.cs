using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonWalker.Obstacles
{
    /// <summary>
    /// Inherits form obstacle, 
    /// Flames that burn on and off at a fixed rate killing the killing the player if they are touched
    /// </summary>
    public class Flame : Obstacle
    {
        private SpriteBatch sb;
        private Texture2D texIn;
        private Texture2D texOut;
        private Vector2 position;
        private bool flamesOut;
        private int switchDelay = 80;
        private int switchCount = 0;
        private int delay;

        private Vector2 dimesion;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private int delayCounter;
        private const int ROWS = 1;
        private const int COLS = 3;

        public bool FlamesOut { get => flamesOut; set => flamesOut = value; }

        /// <summary>
        /// Constructor that loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="tex"> texture of the flame tile </param>
        /// <param name="position"> position of the flame tile </param>
        /// <param name="delay"> delay between animation frames </param>
        public Flame(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, int delay) : base(game, sb, tex, position)
        {
            this.sb = sb;
            this.texIn = tex;
            this.texOut = game.Content.Load<Texture2D>("images/backgroud/flameOut");
            this.position = position;
            this.FlamesOut = false;
            this.delay = delay;
            this.dimesion = new Vector2(texOut.Width / COLS, texOut.Height / ROWS);
            CreateFrames();
        }

        /// <summary>
        /// Creates all the animation frames based of the sprite sheet
        /// </summary>
        private void CreateFrames()
        {
            frames = new List<Rectangle>();

            for (int i = 0; i < ROWS; i++)
            {
                for (int j = 0; j < COLS; j++)
                {
                    int x = j * (int)dimesion.X;
                    int y = i * (int)dimesion.Y;
                    Rectangle r = new Rectangle(x, y, (int)dimesion.X, (int)dimesion.Y);
                    frames.Add(r);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {
            switchCount++;  
            if (switchCount >= switchDelay)
            {
                FlamesOut = true;
                if(switchCount >= switchDelay * 2)
                {
                    FlamesOut = false;
                    switchCount = 0;
                }
            }

            delayCounter++;

            if (delayCounter > delay)
            {
                frameIndex++;
                if (frameIndex > (ROWS * COLS) - 1)
                {
                    frameIndex = 0;
                }
                delayCounter = 0;
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if (FlamesOut)
            {
                sb.Draw(texOut, position, frames[frameIndex], Color.White);
            }
            else
            {
                sb.Draw(texIn, position, Color.White);
            }
            sb.End();
            base.Draw(gameTime);
        }
    }
}
