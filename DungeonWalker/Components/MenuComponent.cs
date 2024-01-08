using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Components
{
    /// <summary>
    /// MenuComponent is used to help switch thorugh different gameScenes
    /// </summary>
    public class MenuComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont regularFont, highlightFont;

        private List<string> menuItems;

        public int SelectedIndex { get; set; }
        private Vector2 position;
        private Color regularColor = Color.Black;
        private Color highlightColor = Color.Red;
        private KeyboardState oldState;

        /// <summary>
        /// Constructor that sets all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="regularFont"> Unselected item font </param>
        /// <param name="highlightFont"> Selected item font </param>
        /// <param name="menus"> All items to be displyed on the menu </param>
        /// <param name="position"> position of the menu component </param>
        public MenuComponent(Game game, SpriteBatch sb, SpriteFont regularFont,
            SpriteFont highlightFont, string[] menus, Vector2 position) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.highlightFont = highlightFont;
            menuItems = menus.ToList();
            this.position = position;
        }
        /// <summary>
        /// Checks user input to scroll through menu options
        /// </summary>
        /// <param name="gameTime"> game time </param>
        public override void Update(GameTime gameTime)
        {
            KeyboardState ks = Keyboard.GetState();
            if (ks.IsKeyDown(Keys.Down) && oldState.IsKeyUp(Keys.Down))
            {
                if (SelectedIndex == menuItems.Count - 1)
                {
                    SelectedIndex = 0;
                }
                else
                {
                    SelectedIndex++;
                }
            }

            if (ks.IsKeyDown(Keys.Up) && oldState.IsKeyUp(Keys.Up))
            {
                SelectedIndex--;
                if (SelectedIndex == -1)
                {
                    SelectedIndex = menuItems.Count - 1;
                }
            }
            oldState = ks;

            base.Update(gameTime);
        }

        /// <summary>
        /// Draws the menu on the game window
        /// </summary>
        /// <param name="gameTime"> game time </param>
        public override void Draw(GameTime gameTime)
        {
            Vector2 tempPos = position;
            sb.Begin();
            for (int i = 0; i < menuItems.Count; i++)
            {
                if (SelectedIndex == i)
                {
                    sb.DrawString(highlightFont, menuItems[i], tempPos, highlightColor);
                    tempPos.Y += highlightFont.LineSpacing;
                }
                else
                {
                    sb.DrawString(regularFont, menuItems[i], tempPos, regularColor);
                    tempPos.Y += regularFont.LineSpacing;

                }
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
