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
    /// An empty tile to fill empty space
    /// </summary>
    public class VoidTile : Obstacle
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;

        /// <summary>
        /// Constructor that loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="tex"> the empty tile texture </param>
        /// <param name="position"> the void tile position </param>
        public VoidTile(Game game, SpriteBatch sb, Texture2D tex, Vector2 position) : base(game, sb, tex, position)
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
