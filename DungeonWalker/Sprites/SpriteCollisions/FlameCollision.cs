using DungeonWalker.Obstacles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Sprites.SpriteCollisions
{
    /// <summary>
    /// Handles all the collisons between the player and flames
    /// </summary>
    public class FlameCollision : GameComponent
    {
        private Player player;
        private List<Flame> flames;
        /// <summary>
        /// Constructor Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="flames"> a list of all the flame objects </param>
        /// <param name="player"> active player object </param>
        public FlameCollision(Game game, List<Flame> flames, Player player) : base(game)
        {
            this.player = player;
            this.flames = flames;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle pBase = player.GetBounds();
            for (int i = 0; i < flames.Count; i++)
            {
                if (pBase.Intersects(flames[i].GetBounds()))
                {
                    if (flames[1].FlamesOut == true)
                    {
                        player.PlayerDeath();
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
