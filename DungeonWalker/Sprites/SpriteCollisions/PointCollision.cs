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
    /// Handles all the collisons between the player and points
    /// </summary>
    public class PointCollision : GameComponent
    {
        private List<Points> points;
        private Player player;

        private const int DISPLACEMNET_S = 10, DISPLACEMENT_L = 20;

        /// <summary>
        /// Constructor Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="points"> a list of all the points objects</param>
        /// <param name="player"> active player object </param>
        public PointCollision(Game game, List<Points> points, Player player) : base(game)
        {
            this.points = points;
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle pBase = player.GetBounds();
            Rectangle pointBase;
            Rectangle pointRect;

            for (int i = 0; i < points.Count; i++)
            {
                pointBase = points[i].GetBounds();

                pointRect = new Rectangle(pointBase.X + DISPLACEMNET_S, pointBase.Y + DISPLACEMNET_S, DISPLACEMENT_L, DISPLACEMENT_L);

                if (pBase.Intersects(pointRect))
                {

                    points[i].Collected = true;
                }
            }
            base.Update(gameTime);
        }
    }
}
