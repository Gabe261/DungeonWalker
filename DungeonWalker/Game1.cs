/* 
 * Dungeon Walker
 * Created on: Sunday Dec. 3rd 2023
 * Gabriel Siewert
 */

using DungeonWalker.Components;
using DungeonWalker.Obstacles;
using DungeonWalker.Scenes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Collections.Generic;
using System.Diagnostics;

namespace DungeonWalker
{
    /// <summary>
    /// The Game1 class the startup class, it initilizes all game scenes
    /// and controls which sences are active
    /// </summary>
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private KeyboardState pks = Keyboard.GetState();

        // Declare all scenes
        private StartScene startScene;
        private HelpScene helpScene;
        private HighScoreScene highScoreScene;
        private CreditScene creditScene;
        private List<PlayScene> rooms;
        private EndScene endScene;

        private float timePassed = 0;
        private bool timerStart = false;

        private int roomIndex = 0;

        private Song BackgoundMusic1;
        private Song BackgoundMusic2;

        private const int LEVEL_LOOP = 11;
        private const int WINDOW_HEIGHT = 1280, WINDOW_WIDTH = 720, GAME_TILE_WIDTH = 32, GAME_TILE_HEIGHT = 18, TILE_WIDTH = 40;

        /// <summary>
        /// Default Game1 constructor
        /// </summary>
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
        /// <summary>
        /// Sets the windows height, width and all public static variables in Shared
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.IsFullScreen = false;
            _graphics.PreferredBackBufferWidth = WINDOW_HEIGHT;
            _graphics.PreferredBackBufferHeight = WINDOW_WIDTH;
            _graphics.ApplyChanges();
            Shared.stage = new Vector2(_graphics.PreferredBackBufferWidth, _graphics.PreferredBackBufferHeight);
            Shared.width = GAME_TILE_WIDTH;
            Shared.height = GAME_TILE_HEIGHT;
            Shared.tileWidth = TILE_WIDTH;
            Shared.deathCount = 0;
            Shared.playerPoints = 0;
            Shared.playerTime = 0;
            base.Initialize();
        }

        // Loads all game scenes 
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            // instanctiate all scenes here
            startScene = new StartScene(this);
            this.Components.Add(startScene);

            helpScene = new HelpScene(this);
            this.Components.Add(helpScene);

            highScoreScene = new HighScoreScene(this);
            this.Components.Add(highScoreScene);

            creditScene = new CreditScene(this);
            this.Components.Add(creditScene);

            endScene = new EndScene(this);
            this.Components.Add(endScene);

            // Generating run
            GenerateLevels();

            startScene.show();

            BackgoundMusic1 = this.Content.Load<Song>("sounds/background1");
            BackgoundMusic2 = this.Content.Load<Song>("sounds/background2");

            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(BackgoundMusic1);
        }
        /// <summary>
        /// Every update of Game1 controls the flow of game scenes
        /// </summary>
        /// <param name="gameTime"> Game time </param>
        protected override void Update(GameTime gameTime)
        {
            // Starts the current runs timer
            if (timerStart)
            {
                timePassed += (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            int startIndex = 0;
            int endIndex = 0;
            KeyboardState ks = Keyboard.GetState();
            // Start scene enabled
            if (startScene.Enabled)
            {
                startIndex = startScene.Menu.SelectedIndex;
                if (startIndex == 0 && ks.IsKeyDown(Keys.Enter))
                {
                    MediaPlayer.Pause();
                    MediaPlayer.Play(BackgoundMusic2);
                    startScene.hide();
                    rooms[0].show();
                }
                else if (startIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    helpScene.show();
                }
                else if (startIndex == 2 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    highScoreScene.show();
                }
                else if (startIndex == 3 && ks.IsKeyDown(Keys.Enter))
                {
                    startScene.hide();
                    creditScene.show();
                }
                else if (startIndex == 4 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            // A level's room enabled
            if (rooms[roomIndex].Enabled)
            {
                timerStart = true;
                if (rooms[roomIndex].SceneEnd == true)
                {
                    rooms[roomIndex].hide();

                    roomIndex++;
                    if (roomIndex == rooms.Count - 1)
                    {
                        GenerateLevels();
                        timerStart = false;
                        Shared.playerTime = timePassed;
                        endScene.DisplayScore();
                        endScene.show();
                    }
                    else
                    {
                        rooms[roomIndex].show();
                    }
                }
                if (ks.IsKeyDown(Keys.Escape))
                {
                    timerStart = false;
                    rooms[roomIndex].hide();
                    GenerateLevels();
                    MediaPlayer.Pause();
                    MediaPlayer.Play(BackgoundMusic1);
                    startScene.show();
                }
            }
            else if (helpScene.Enabled) // Help scene enabled
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    helpScene.hide();
                    startScene.show();
                }
            }
            else if (highScoreScene.Enabled) // highscore scene enabled
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    highScoreScene.hide();
                    startScene.show();
                }
            }
            else if (creditScene.Enabled)  // credit scene enabled
            {
                if (ks.IsKeyDown(Keys.Escape))
                {
                    creditScene.hide();
                    startScene.show();
                }
            }
            else if (endScene.Enabled)  // end scene enabled
            {
                endIndex = endScene.Menu.SelectedIndex;
                if (endIndex == 0 && pks.IsKeyDown(Keys.Enter) && ks.IsKeyUp(Keys.Enter))
                {
                    endScene.ScoreSaved = false;
                    endScene.hide();
                    MediaPlayer.Pause();
                    MediaPlayer.Play(BackgoundMusic1);
                    startScene.show();
                }
                else if (endIndex == 1 && ks.IsKeyDown(Keys.Enter))
                {
                    Exit();
                }
            }
            pks = ks;
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }

        /// <summary>
        /// Adds unique versions of the PlayScene to be iterated through when playing the game
        /// </summary>
        private void GenerateLevels()
        {
            RoomDesigner ld = new RoomDesigner(this);
            rooms = new List<PlayScene>();

            for (int i = 0; i < LEVEL_LOOP; i++)
            {
                Obstacle[,] obstacles = ld.GenerateRoom();
                Vector2 endPos = ld.GetEndPosition();
                Vector2 startPos = ld.GetStartPosition();
                PlayScene actionScene2 = new PlayScene(this, startPos, endPos, obstacles);
                rooms.Add(actionScene2);
                this.Components.Add(actionScene2);
            }

            roomIndex = 0;
        }
    }
}