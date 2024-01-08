using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonWalker.Obstacles
{
    /// <summary>
    /// Inherits form obstacle,
    /// spikes the pop out if the player walks over them and kills them if the player stays
    /// </summary>
    public class Spike : Obstacle
    {
        private SpriteBatch sb;
        private Texture2D texIn;
        private Texture2D texOut;
        private Vector2 position;
        private bool spikesTripped;
        private bool spikesOut;
        private int stabDelay;

        public bool SpikesTripped { get => spikesTripped; set => spikesTripped = value; }
        public bool SpikesOut { get => spikesOut; set => spikesOut = value; }

        /// <summary>
        /// Constructor that loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch variable </param>
        /// <param name="tex"> texture of the spikes </param>
        /// <param name="position"> positon of the spikes </param>
        public Spike(Game game, SpriteBatch sb, Texture2D tex, Vector2 position) : base(game, sb, tex, position)
        {
            this.sb = sb;
            this.texIn = tex;
            this.texOut = game.Content.Load<Texture2D>("images/backgroud/spikesOut");
            this.position = position;
            this.SpikesTripped = false;
            this.SpikesOut = false;
            this.stabDelay = 0;
        }

        public override void Update(GameTime gameTime)
        {
            // Spikes pop out if they have been tripped
            if(SpikesTripped)
            {
                stabDelay++;
                if(stabDelay == 30)
                {
                    SpikesOut = true;
                    
                } else if(stabDelay == 100)
                {
                    spikesTripped = false;
                    spikesOut = false;
                    stabDelay = 0;  
                }
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            if (SpikesOut)
            {
                sb.Draw(texOut, position, Color.White);
            }
            else
            {
                sb.Draw(texIn, position, Color.White);
            }
            sb.End();

            base.Draw(gameTime);
        }
    }
}
