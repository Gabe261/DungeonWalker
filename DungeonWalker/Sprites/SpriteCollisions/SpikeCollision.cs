using DungeonWalker.Obstacles;
using Microsoft.Xna.Framework;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Sprites.SpriteCollisions
{
    /// <summary>
    /// Handles all the collisons between the player and spikes
    /// </summary>
    public class SpikeCollision : GameComponent
    {
        private List<Spike> spikes;
        private Player player;
        /// <summary>
        /// Constructor Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="spikes"> a list of all the spike objects </param>
        /// <param name="player"> the active player object </param>
        public SpikeCollision(Game game, List<Spike> spikes, Player player) : base(game)
        {
            this.spikes = spikes;
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle pBase = player.GetBounds();
            for (int i = 0; i < spikes.Count; i++)
            {
                if (pBase.Intersects(spikes[i].GetBounds()))
                {
                    spikes[i].SpikesTripped = true;
                    if (spikes[i].SpikesOut == true)
                    {
                        player.PlayerDeath();
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
