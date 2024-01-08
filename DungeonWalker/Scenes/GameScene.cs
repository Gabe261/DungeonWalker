using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// A parent class for all the scene found in Dungeon Walker
    /// </summary>
    public abstract class GameScene : DrawableGameComponent
    {
        public List<GameComponent> Components { get; set; }

        /// <summary>
        /// Constructor that creates a list of game components
        /// </summary>
        /// <param name="game"> Game variable </param>
        protected GameScene(Game game) : base(game)
        {
            Components = new List<GameComponent>();
            hide();
        }

        /// <summary>
        /// Hides and disables the gamescene so another can be played
        /// </summary>
        public virtual void hide()
        {
            this.Enabled = false;
            this.Visible = false;
        }

        /// <summary>
        /// Shows the gamescene to that it can be played
        /// </summary>
        public virtual void show()
        {
            this.Enabled = true;
            this.Visible = true;
        }

        public override void Update(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item.Enabled)
                {
                    item.Update(gameTime);
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            foreach (GameComponent item in Components)
            {
                if (item is DrawableGameComponent)
                {
                    DrawableGameComponent comp = (DrawableGameComponent)item;
                    if (comp.Visible)
                    {
                        comp.Draw(gameTime);
                    }
                }
            }
            base.Draw(gameTime);
        }
    }
}
