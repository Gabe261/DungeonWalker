using DungeonWalker.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// Inherits from gameScene,
    /// Dipsplays all the highscores that have been acheived
    /// </summary>
    public class HighScoreScene : GameScene
    {
        private Texture2D tex;
        private SpriteBatch sb;
        Game1 g;
        private HighScoreComponent component;

        private const int LINE_DISPLACMENT = 100, NEW_LINE = 30;
        private int newLine = LINE_DISPLACMENT;

        /// <summary>
        /// Loads all the classes variables
        /// </summary>
        /// <param name="game"></param>
        public HighScoreScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            sb = g._spriteBatch;
            component = new HighScoreComponent(game, g._spriteBatch, g.Content.Load<SpriteFont>("fonts/RegularFont"));
            tex = game.Content.Load<Texture2D>("images/background");
        }

        public override void Update(GameTime gameTime)
        {
            component.ReadFromFileToList();
            base.Update(gameTime);
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            newLine = LINE_DISPLACMENT;

            foreach (string s in component.Scores)
            {
                if (s != null)
                {
                    newLine += NEW_LINE;
                    sb.DrawString(g.Content.Load<SpriteFont>("fonts/RegularFont"), s, new Vector2(LINE_DISPLACMENT, newLine), Color.White);
                }
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
