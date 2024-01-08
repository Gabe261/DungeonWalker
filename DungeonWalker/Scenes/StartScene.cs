using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DungeonWalker.Components;
using static System.Net.Mime.MediaTypeNames;
using Microsoft.Xna.Framework.Media;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// Inherits from gameScene,
    /// The main menu that allows users to go to all other scenes
    /// </summary>
    public class StartScene : GameScene
    {
        private MenuComponent menu;
        private SpriteBatch sb;
        private Game1 g;
        private const int MENU_X = 200;

        public MenuComponent Menu { get => menu; set => menu = value; }

        /// <summary>
        /// Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        public StartScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.sb = g._spriteBatch;
            string[] menuItems = new string[] { "Start Run", "Help", "High Score", "Credits", "Quit" };

            SpriteFont regularFont = g.Content.Load<SpriteFont>("fonts/RegularFont");
            SpriteFont highlightFont = g.Content.Load<SpriteFont>("fonts/HighlightFont");

            Menu = new MenuComponent(game, sb, regularFont, highlightFont, menuItems, new Vector2(MENU_X, Shared.stage.Y / 2));
            this.Components.Add(Menu);

            Song BackgoundMusic = game.Content.Load<Song>("sounds/background1");
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BackgoundMusic);
            MediaPlayer.Pause();
        }
        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(g.Content.Load<Texture2D>("images/background"), Vector2.Zero, Color.White);
            sb.Draw(g.Content.Load<Texture2D>("images/titleCard"), Vector2.Zero, Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
