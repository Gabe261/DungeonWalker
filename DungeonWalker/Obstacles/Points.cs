using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Obstacles
{
    /// <summary>
    /// Inherits form obstacle, 
    /// points that will be used for player score the disapre when collected
    /// </summary>
    public class Points : Obstacle
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;
        private int delay;

        private Vector2 dimesion;
        private List<Rectangle> frames;
        private int frameIndex = 0;

        private int delayCounter;
        private const int ROWS = 1;
        private const int COLS = 4;

        private bool collected;
        private bool addPoints;
        private Texture2D voidTile;

        private SoundEffect collectSound;

        public bool Collected { get => collected; set => collected = value; }

        /// <summary>
        /// Constructor that loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="tex"> texture of the points </param>
        /// <param name="position"> position of the points </param>
        /// <param name="delay"> delay of the point animation </param>
        public Points(Game game, SpriteBatch sb, Texture2D tex, Vector2 position, int delay) : base(game, sb, tex, position)
        {
            Game1 g = (Game1)game;
            this.sb = sb;
            this.tex = tex;
            this.position = position;
            this.delay = delay;
            this.dimesion = new Vector2(tex.Width / COLS, tex.Height / ROWS);
            this.Collected = false;
            this.addPoints = false;
            this.voidTile = g.Content.Load<Texture2D>("images/backgroud/void");
            this.collectSound = game.Content.Load<SoundEffect>("sounds/collect");
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
                    // g.Components.Remove(this);
                }
            }
        }

        public override void Update(GameTime gameTime)
        {

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
            if (Collected && addPoints == false)
            {
                addPoints = true;
                Shared.playerPoints += 10;
                collectSound.Play();
            }

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if (Collected)
            {
                sb.Draw(voidTile, position, Color.White);
            }
            else
            {
                sb.Draw(tex, position, frames[frameIndex], Color.White);

            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
