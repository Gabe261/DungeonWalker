using DungeonWalker.Components;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic.Devices;
using System.Windows.Forms;
using Microsoft.Xna.Framework.Input;
using System.IO;

namespace DungeonWalker.Scenes
{
    /// <summary>
    /// Inherits from gameScene,
    /// 
    /// </summary>
    public class EndScene : GameScene
    {
        private Texture2D tex;
        private Microsoft.Xna.Framework.Graphics.SpriteBatch sb;
        private SpriteFont font, highliFont;
        private Game1 g;
        private HighScoreComponent component;
        private MenuComponent menu;
        private int playerDeaths;
        private int playerPoints;
        private float playerTime;
        private int playerScore;
        private bool scoreSaved;

        private const int MENU_X = 600, MENU_Y = 500;
        private const int SCORE_X = 700, SCORE_Y = 100;
        private const int DEATH_DEDUCTION = 50, TIME_ITERATION = 30;
        private const int LINE_DISPLACMENT = 100, NEW_LINE = 30;
        private int newLine = LINE_DISPLACMENT;

        private string lineToRight;

        public MenuComponent Menu { get => menu; set => menu = value; }
        public bool ScoreSaved { get => scoreSaved; set => scoreSaved = value; }

        /// <summary>
        /// Loads all the classes variables
        /// </summary>
        /// <param name="game"> Game variable </param>
        public EndScene(Game game) : base(game)
        {
            this.g = (Game1)game;
            this.sb = g._spriteBatch;
            tex = game.Content.Load<Texture2D>("images/background");
            this.font = g.Content.Load<SpriteFont>("fonts/RegularFont");
            this.highliFont = g.Content.Load<SpriteFont>("fonts/HighlightFont");
            this.ScoreSaved = false;

            component = new HighScoreComponent(game, g._spriteBatch, g.Content.Load<SpriteFont>("fonts/RegularFont"));

            string[] menuItems = new string[] { "Main Menu", "Quit" };

            Menu = new MenuComponent(game, g._spriteBatch, font, highliFont, menuItems, new Vector2(MENU_X, MENU_Y));
            this.Components.Add(Menu);
        }


        public void DisplayScore()
        {
            this.playerTime = Shared.playerTime;
            this.playerDeaths = Shared.deathCount;
            this.playerPoints = Shared.playerPoints;

            this.playerScore = 0;

            int timeScore = (int)playerTime / TIME_ITERATION;
            int timeMultiplier = 0;

            lineToRight = "";

            lineToRight += "You finished in " + playerTime.ToString("N2") + " seconds";

            switch (timeScore)
            {
                case 0:
                    timeMultiplier = 9;
                    break;
                case 1:
                    timeMultiplier = 8;
                    break;
                case 2:
                    timeMultiplier = 7;
                    break;
                case 3:
                    timeMultiplier = 6;
                    break;
                case 4:
                    timeMultiplier = 5;
                    break;
                case 5:
                    timeMultiplier = 4;
                    break;
                case 6:
                    timeMultiplier = 3;
                    break;
                case 7:
                    timeMultiplier = 2;
                    break;
                case 8:
                    timeMultiplier = 1;
                    break;
                default:
                    timeMultiplier = 1;
                    break;
            }

            lineToRight += "\nso you recived a time multiplier of " + timeMultiplier;
            lineToRight += "\n\nYou collected " + playerPoints + " points";

            int deathSubtraction = playerDeaths * DEATH_DEDUCTION;

            lineToRight += "\n\nYou died " + playerDeaths + " times\nso you recived a score deduciton of -" + deathSubtraction;

            playerScore = (playerPoints * timeMultiplier) - deathSubtraction;
            lineToRight += "\n----------------------------------------------------------";
            lineToRight += $"\nYour total score is: {playerPoints} x {timeMultiplier} - {deathSubtraction} = {playerScore}";
            lineToRight += $"\n\nPress SPACE to save score as: Anonymous";

            Vector2 topRight = new Vector2(Shared.stage.X - SCORE_X, SCORE_Y);
            DisplayString ScoreText = new DisplayString(g, sb, lineToRight, font, topRight, Color.White);
        }

        /// <summary>
        /// Writes the current players name to the file along with thier score
        /// </summary>
        /// <param name="playerName"> The name of the player </param>
        private void RecordRecord(string playerName)
        {
            try
            {
                StreamWriter writer = new StreamWriter("highscores.txt", true);

                writer.WriteLine($"{playerName} --- Score : {playerScore}");
                writer.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public override void Update(GameTime gameTime)
        {
            component.ReadFromFileToList();
            Debug.WriteLine("HERE ==== " + ScoreSaved);
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Space) && ScoreSaved == false)
            {
                ScoreSaved = true;
                RecordRecord("Anonymous");
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            sb.Begin();
            sb.Draw(tex, Vector2.Zero, Color.White);
            newLine = LINE_DISPLACMENT;

            foreach (string s in component.Scores)
            {
                if (s != null)
                {
                    newLine += NEW_LINE;
                    sb.DrawString(g.Content.Load<SpriteFont>("fonts/RegularFont"), s, new Vector2(100, newLine), Color.White);
                }
            }
            sb.DrawString(g.Content.Load<SpriteFont>("fonts/RegularFont"), lineToRight, new Vector2(Shared.stage.X - SCORE_X, SCORE_Y), Color.White);
            sb.End();

            base.Draw(gameTime);
        }
    }
}
