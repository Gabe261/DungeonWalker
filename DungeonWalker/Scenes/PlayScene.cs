using DungeonWalker.Obstacles;
using DungeonWalker.Sprites;
using DungeonWalker.Sprites.SpriteCollisions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// Inherits from gameScene,
    /// The level scene where the core of the game is played, generated to be new each time
    /// </summary>
    public class PlayScene : GameScene
    {
        SpriteBatch sb;
        private Game1 g;
        private Player p;
        private bool sceneEnd;
        private Vector2 startPos;
        private Vector2 endPos;
        private Obstacle[,] level;

        private const int DOWN = 0, RIGHT = 1, LEFT = 2, UP = 3;
        private const float PLAYER_SPEED = 5f;

        public bool SceneEnd { get => sceneEnd; set => sceneEnd = value; }

        /// <summary>
        /// Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="startPos"> The starting position of the level </param>
        /// <param name="endPos"> The ending position of the level </param>
        /// <param name="level"> The 2D obstacle array that will be displayed on the PlayScene window </param>
        public PlayScene(Game game, Vector2 startPos, Vector2 endPos, Obstacle[,] level) : base(game)
        {
            this.g = (Game1)game;
            this.sb = g._spriteBatch;
            SceneEnd = false;
            this.startPos = startPos;
            this.endPos = endPos;
            this.level = level;

            Texture2D[] playerTextures = new Texture2D[4];
            playerTextures[DOWN] = game.Content.Load<Texture2D>("images/player/playerDown"); // Down
            playerTextures[RIGHT] = game.Content.Load<Texture2D>("images/player/playerRight"); // Right
            playerTextures[LEFT] = game.Content.Load<Texture2D>("images/player/playerLeft"); // Left
            playerTextures[UP] = game.Content.Load<Texture2D>("images/player/playerUp"); // Up
            float playerSpeed = PLAYER_SPEED;

            p = new Player(game, sb, playerTextures, startPos, playerSpeed);

            List<Wall> walls = new List<Wall>();
            List<Spike> spikes = new List<Spike>();
            List<Flame> flames = new List<Flame>();
            List<Points> points = new List<Points>();
            // Adds all walls, spikes, flames and points to list to be added with collison managers
            for (int i = 0; i < level.GetLength(0); i++)
            {
                for (int j = 0; j < level.GetLength(1); j++)
                {
                    this.Components.Add(level[i, j]);
                    if (level[i, j] is Wall)
                    {
                        walls.Add((Wall)level[i, j]);
                    }
                    else if(level[i, j] is Spike)
                    {
                        spikes.Add((Spike)level[i, j]);
                    }
                    else if (level[i, j] is Flame)
                    {
                        flames.Add((Flame)level[i, j]);
                    }
                    else if (level[i, j] is Points)
                    {
                        points.Add((Points)level[i, j]);
                    }
                }
            }
            VoidTile finishTile = new VoidTile(game, sb, game.Content.Load<Texture2D>("images/backgroud/finishTile"), endPos);
            // Collision of each tile
            WallCollision wallCollsions = new WallCollision(game, walls, p);
            SpikeCollision spikeCollsions = new SpikeCollision(game, spikes, p);
            FlameCollision flameCollsions = new FlameCollision(game, flames, p);
            PointCollision pointsCollsions = new PointCollision(game, points, p);

            // ALl compents added to the scene
            this.Components.Add(wallCollsions);
            this.Components.Add(spikeCollsions);
            this.Components.Add(flameCollsions);
            this.Components.Add(pointsCollsions);
            this.Components.Add(finishTile);
            this.Components.Add(p);
        }

        public override void Update(GameTime gameTime)
        {
            Rectangle playerRect = p.GetBounds();
            Rectangle finishRect = new Rectangle((int)endPos.X, (int)endPos.Y, Shared.tileWidth, Shared.tileWidth);
            if (playerRect.Intersects(finishRect))
            {
                SceneEnd = true;
            }
            base.Update(gameTime);
        }
    }
}
