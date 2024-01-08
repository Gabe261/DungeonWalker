using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Obstacles
{
    /// <summary>
    /// Inherits form obstacle,
    /// Walls direct player and stop them from moving through they're occupying tile
    /// </summary>
    public class Wall : Obstacle
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;

        /// <summary>
        /// Constructor that loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="tex"> Texture of the wall </param>
        /// <param name="position"> position of the wall </param>
        public Wall(Game game, SpriteBatch sb, Texture2D tex, Vector2 position) : base(game, sb, tex, position)
        {
            this.sb = sb;
            this.tex = tex;
            this.position = position;
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, position, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
