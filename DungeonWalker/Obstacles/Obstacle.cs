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
    /// The parent class to the the obsacle tiles that appear in a room
    /// </summary>
    public class Obstacle : DrawableGameComponent
    {
        private SpriteBatch sb;
        private Texture2D tex;
        private Vector2 position;

        /// <summary>
        /// Constructor that loads all the classes properties
        /// </summary>
        /// <param name="game"></param>
        /// <param name="sb"></param>
        /// <param name="tex"></param>
        /// <param name="position"></param>
        public Obstacle(Game game, SpriteBatch sb, Texture2D tex, Vector2 position) : base(game)
        {
            this.sb = sb;
            this.tex = tex;
            this.position = position;
        }

        /// <summary>
        /// Makes a rectangle based of the tiles position and texture
        /// </summary>
        /// <returns> a rectangle surrounding and representing the obstacle </returns>
        public Rectangle GetBounds()
        {
            return new Rectangle((int)position.X, (int)position.Y, tex.Width, tex.Height);
        }
    }
}
