using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// Inherits from gameScene,
    /// Displays the games information on how to play
    /// </summary>
    public class HelpScene : GameScene
    {
        private Texture2D background;
        private Texture2D texText;
        private SpriteBatch sb;

        /// <summary>
        /// Loads all the classes variables
        /// </summary>
        /// <param name="game"></param>
        public HelpScene(Game game) : base(game)
        {
            Game1 g = (Game1)game;
            sb = g._spriteBatch;
            background = game.Content.Load<Texture2D>("images/background");
            texText = game.Content.Load<Texture2D>("images/helpPage");
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(background, Vector2.Zero, Color.White);
            sb.Draw(texText, Vector2.Zero, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
