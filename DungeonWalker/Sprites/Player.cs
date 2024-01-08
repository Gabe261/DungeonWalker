using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace DungeonWalker.Sprites
{
    /// <summary>
    /// The player class that holds the main player in Dungeon Walker
    /// </summary>
    public class Player : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D[] texs;
        private Texture2D currTex;
        private Vector2 pos;
        private float speed;
        private Color playerCol;
        private Vector2 startPos;
        private SoundEffect deathSound;

        private const int UP = 3, DOWN = 0, LEFT = 2, RIGHT = 1;
        private const int DIS_S = 2, DIS_L = 4;

        private bool rightMove, leftMove, upMove, downMove;
        public bool RightMove { get => rightMove; set => rightMove = value; }
        public bool LeftMove { get => leftMove; set => leftMove = value; }
        public bool UpMove { get => upMove; set => upMove = value; }
        public bool DownMove { get => downMove; set => downMove = value; }

        /// <summary>
        /// Constructor that loads all the player variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable</param>
        /// <param name="texs"> The array up player textures </param>
        /// <param name="pos"> the starting postion the player </param>
        /// <param name="speed"> the speed at which the player moves </param>
        public Player(Game game, SpriteBatch sb, Texture2D[] texs, Vector2 pos, float speed) : base(game)
        {
            this.sb = sb;
            this.texs = texs;
            this.currTex = texs[DOWN];
            this.pos = pos;
            this.startPos = pos;
            this.speed = speed;
            this.RightMove = true;
            this.LeftMove = true;
            this.UpMove = true;
            this.DownMove = true;
            this.playerCol = Color.White;
            this.deathSound = game.Content.Load<SoundEffect>("sounds/hit");
        }
        public override void Update(GameTime gameTime)
        {

            if (pos.X < 0) // Left
                LeftMove = false;

            if (pos.X > Shared.stage.X - currTex.Width) // Right
                RightMove = false;

            if (pos.Y < 0) // Up
                UpMove = false;

            if (pos.Y > Shared.stage.Y - currTex.Height) // Down    
                DownMove = false;



            KeyboardState ks = Keyboard.GetState();
            float xDir = 0.0f;
            float yDir = 0.0f;

            Vector2 movement = new Vector2(xDir, yDir);

            if (ks.IsKeyDown(Keys.S) && DownMove == true) // Down
            {
                movement.Y += speed;
                currTex = texs[DOWN];
            }
            if (ks.IsKeyDown(Keys.W) && UpMove == true) // Up
            {
                movement.Y -= speed;
                currTex = texs[UP];
            }
            if (ks.IsKeyDown(Keys.A) && LeftMove == true) // Left
            {
                movement.X -= speed;
                currTex = texs[LEFT];
            }
            if (ks.IsKeyDown(Keys.D) && RightMove == true) // Right
            {
                movement.X += speed;
                currTex = texs[RIGHT];
            }

            if (movement.X != 0f && movement.Y != 0f)
            {
                if (movement.X > 0f && movement.Y > 0f) // Down Right
                {
                    movement.X -= 1;
                    movement.Y -= 1;
                }
                if (movement.X > 0f && movement.Y < 0f) // Up Right
                {
                    movement.X -= 1;
                    movement.Y += 1;
                }
                if (movement.X < 0f && movement.Y > 0f) // Down Left
                {
                    movement.X += 1;
                    movement.Y -= 1;
                }
                if (movement.X < 0f && movement.Y < 0f) // Up Left
                {
                    movement.X += 1;
                    movement.Y += 1;
                }
            }

            pos += movement;

            LeftMove = true;
            RightMove = true;
            UpMove = true;
            DownMove = true;

            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(currTex, pos, playerCol);
            sb.End();
            base.Draw(gameTime);
        }

        /// <summary>
        /// When a player hits an obstable that kills the player their position is reset and a sound effect is played
        /// </summary>
        public void PlayerDeath()
        {
            pos = startPos;
            Shared.deathCount++;
            deathSound.Play();
        }

        /// <summary>
        /// The bounds of the current players texture
        /// </summary>
        /// <returns> A rectangle based on the players position and dimesions </returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)pos.X - DIS_S, (int)pos.Y - DIS_S, currTex.Width + DIS_L, currTex.Height + DIS_L);
        }
    }
}
