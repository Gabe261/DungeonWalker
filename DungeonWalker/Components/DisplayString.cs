using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Components
{
    /// <summary>
    /// DisplayString is a helper class to draw a message in the game window
    /// </summary>
    public class DisplayString : DrawableGameComponent
    {
        private SpriteBatch sb;
        private string message;
        private SpriteFont font;
        private Vector2 position;
        private Color color;

        public Vector2 Position { get => position; set => position = value; }
        public string Message { get => message; set => message = value; }

        /// <summary>
        /// Constructor of DisplayString sets the classes methods
        /// </summary>
        /// <param name="game"> Game variable inherited from DrawableGameComponent </param>
        /// <param name="sb"> sprite batch variable </param>
        /// <param name="message"> message to be displayed </param>
        /// <param name="font"> font the message will use </param>
        /// <param name="position"> position of the message </param>
        /// <param name="color"> color of the message text </param>
        public DisplayString(Game game, SpriteBatch sb, string message, SpriteFont font, Vector2 position, Color color) : base(game)
        {
            this.sb = sb;
            this.Message = message;
            this.font = font;
            this.Position = position;
            this.color = color;
        }

        /// <summary>
        /// Draws the message on the game window
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.DrawString(font, Message, Position, color);
            sb.End();
            base.Draw(gameTime);
        }
    }
}
