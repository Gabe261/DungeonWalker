using DungeonWalker.Obstacles;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Sprites.SpriteCollisions
{
    /// <summary>
    /// Handles all the collisons between the player and walls
    /// </summary>
    public class WallCollision : GameComponent
    {
        private List<Wall> walls;
        private Player player;

        private const int DISPLACEMNET_S = 6, DISPLACEMENT_L = 12;

        /// <summary>
        /// Constructor Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="walls"> a list of all the wall objects </param>
        /// <param name="player"> the active players object </param>
        public WallCollision(Game game, List<Wall> walls, Player player) : base(game)
        {
            this.walls = walls;
            this.player = player;
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle pBase = player.GetBounds();

            int displacetS = DISPLACEMNET_S;
            int displacetL = DISPLACEMENT_L;

            Rectangle pTop = new Rectangle(pBase.X + displacetS, pBase.Y, pBase.Width - displacetL, displacetS);
            Rectangle pBot = new Rectangle(pBase.X + displacetS, pBase.Y + pBase.Height - displacetS, pBase.Width - displacetL, displacetS);
            Rectangle pLeft = new Rectangle(pBase.X, pBase.Y + displacetS, displacetS, pBase.Height - displacetL);
            Rectangle pRight = new Rectangle(pBase.X + pBase.Width - displacetS, pBase.Y + displacetS, displacetS, pBase.Height - displacetL);


            for (int i = 0; i < walls.Count; i++)
            {
                Intercepts(pTop, pBot, pLeft, pRight, walls[i]);
            }
            base.Update(gameTime);
        }

        /// <summary>
        /// Restricts the players movement base off which direction the player is moving
        /// </summary>
        /// <param name="r1"> player top hitbox </param>
        /// <param name="r2"> player bottom hitbox </param>
        /// <param name="r3"> player left hitbox </param>
        /// <param name="r4"> player right hitbox </param>
        /// <param name="w"> wall hitbox </param>
        public void Intercepts(Rectangle r1, Rectangle r2, Rectangle r3, Rectangle r4, Obstacle w)
        {
            if (r1.Intersects(w.GetBounds()))
                player.UpMove = false;

            if (r2.Intersects(w.GetBounds()))
                player.DownMove = false;

            if (r3.Intersects(w.GetBounds()))
                player.LeftMove = false;

            if (r4.Intersects(w.GetBounds()))
                player.RightMove = false;
        }
    }
}
